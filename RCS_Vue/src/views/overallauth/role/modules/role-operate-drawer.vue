<script setup lang="ts">
import { computed, reactive, ref, watch } from 'vue';
import type { TreeOption } from 'naive-ui';
import { fetchAddRole, fetchGetPermissions, fetchUpdatePermissions, fetchUpdateRole } from '@/service/api';
import { useFormRules, useNaiveForm } from '@/hooks/common/form';
import { $t } from '@/locales';

defineOptions({
  name: 'RoleOperateDrawer'
});

const props = defineProps<{
  operateType: NaiveUI.TableOperateType;
  rowData?: Api.Rbac.Role | null;
}>();

const emit = defineEmits<{
  (e: 'submitted'): void;
}>();

const visible = defineModel<boolean>('visible', {
  default: false
});

const { formRef, validate, restoreValidation } = useNaiveForm();

type Model = Pick<Api.Rbac.RoleEdit, 'name' | 'isDefault' | 'isPublic'>;

const model: Model = reactive(createDefaultModel());
const loadingPermissions = ref(false);
const permissionGroups = ref<Api.Rbac.PermissionGroup[]>([]);
const checkedPermissions = ref<string[]>([]);

const title = computed(() => (props.operateType === 'add' ? '新增角色' : '编辑角色'));

const rules = computed<Record<keyof Pick<Model, 'name'>, App.Global.FormRule>>(() => {
  const { defaultRequiredRule } = useFormRules();

  return {
    name: defaultRequiredRule
  };
});

const permissionTreeData = computed<TreeOption[]>(() =>
  permissionGroups.value.map(group => ({
    key: group.name,
    label: group.displayName || group.name,
    disabled: true,
    children: group.permissions.map(permission => ({
      key: permission.name,
      label: permission.displayName || permission.name
    }))
  }))
);

function createDefaultModel(): Model {
  return {
    name: '',
    isDefault: false,
    isPublic: false
  };
}

function extractGrantedPermissions(groups: Api.Rbac.PermissionGroup[]) {
  checkedPermissions.value = groups.flatMap(group =>
    group.permissions.filter(permission => permission.isGranted).map(permission => permission.name)
  );
}

async function fetchRolePermissions(roleName: string) {
  loadingPermissions.value = true;
  try {
    const { data, error } = await fetchGetPermissions('R', roleName);
    if (!error && data) {
      permissionGroups.value = data.groups || [];
      extractGrantedPermissions(permissionGroups.value);
    }
  } finally {
    loadingPermissions.value = false;
  }
}

async function fetchPermissionDefinitions() {
  loadingPermissions.value = true;
  try {
    const { data, error } = await fetchGetPermissions('R', model.name || 'template');
    if (!error && data) {
      permissionGroups.value = data.groups || [];
      checkedPermissions.value = [];
    }
  } finally {
    loadingPermissions.value = false;
  }
}

async function updateModel() {
  if (props.operateType === 'add') {
    Object.assign(model, createDefaultModel());
    checkedPermissions.value = [];
    await fetchPermissionDefinitions();
    return;
  }

  if (props.rowData) {
    Object.assign(model, {
      name: props.rowData.name,
      isDefault: props.rowData.isDefault,
      isPublic: props.rowData.isPublic
    });
    await fetchRolePermissions(props.rowData.name);
  }
}

function closeDrawer() {
  visible.value = false;
}

function buildPermissionPayload(): Api.Rbac.UpdatePermissionsRequest {
  const checked = new Set(checkedPermissions.value);
  return {
    permissions: permissionGroups.value.flatMap(group =>
      group.permissions.map(permission => ({
        name: permission.name,
        isGranted: checked.has(permission.name)
      }))
    )
  };
}

async function handleSubmit() {
  await validate();

  const submitData: Api.Rbac.RoleEdit = {
    name: model.name,
    isDefault: model.isDefault,
    isPublic: model.isPublic
  };

  if (props.operateType === 'edit' && props.rowData) {
    submitData.concurrencyStamp = props.rowData.concurrencyStamp;
  }

  if (props.operateType === 'edit' && !props.rowData) {
    return;
  }

  const result =
    props.operateType === 'add' ? await fetchAddRole(submitData) : await fetchUpdateRole(props.rowData!.id, submitData);

  if (result.error) {
    return;
  }

  const roleName = props.operateType === 'add' ? result.data?.name || model.name : props.rowData?.name;
  if (roleName) {
    const permissionResult = await fetchUpdatePermissions('R', roleName, buildPermissionPayload());
    if (permissionResult.error) {
      window.$message?.warning('角色保存成功，但权限授权失败');
      return;
    }
  }

  window.$message?.success($t('common.updateSuccess'));
  closeDrawer();
  emit('submitted');
}

watch(visible, () => {
  if (visible.value) {
    updateModel();
    restoreValidation();
  }
});
</script>

<template>
  <NDrawer v-model:show="visible" display-directive="show" :width="520">
    <NDrawerContent :title="title" :native-scrollbar="false" closable>
      <NForm ref="formRef" :model="model" :rules="rules" label-placement="left" :label-width="96">
        <NFormItem label="角色名称" path="name">
          <NInput v-model:value="model.name" :disabled="operateType === 'edit' && rowData?.isStatic" placeholder="请输入角色名称" />
        </NFormItem>
        <NFormItem label="默认角色" path="isDefault">
          <NSwitch v-model:value="model.isDefault">
            <template #checked>是</template>
            <template #unchecked>否</template>
          </NSwitch>
        </NFormItem>
        <NFormItem label="公开角色" path="isPublic">
          <NSwitch v-model:value="model.isPublic">
            <template #checked>是</template>
            <template #unchecked>否</template>
          </NSwitch>
        </NFormItem>
        <NFormItem label="权限授权">
          <NSpin :show="loadingPermissions" class="w-full">
            <NTree
              v-model:checked-keys="checkedPermissions"
              :data="permissionTreeData"
              checkable
              cascade
              block-line
              expand-on-click
              default-expand-all
              class="permission-tree"
            />
          </NSpin>
        </NFormItem>
      </NForm>
      <template #footer>
        <NSpace :size="16">
          <NButton @click="closeDrawer">{{ $t('common.cancel') }}</NButton>
          <NButton type="primary" @click="handleSubmit">{{ $t('common.confirm') }}</NButton>
        </NSpace>
      </template>
    </NDrawerContent>
  </NDrawer>
</template>

<style scoped>
.permission-tree {
  max-height: 420px;
  width: 100%;
  overflow: auto;
  border: 1px solid var(--n-border-color);
  border-radius: 4px;
  padding: 8px;
}
</style>
