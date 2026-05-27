<script setup lang="ts">
import { computed, reactive, ref, watch } from 'vue';
import { fetchAddUser, fetchAssignRolesToUser, fetchGetAssignableRoles, fetchGetUserRoles, fetchUpdateUser } from '@/service/api';
import { useFormRules, useNaiveForm } from '@/hooks/common/form';
import { $t } from '@/locales';

defineOptions({
  name: 'UserOperateDrawer'
});

const props = defineProps<{
  operateType: NaiveUI.TableOperateType;
  rowData?: Api.Rbac.User | null;
}>();

const emit = defineEmits<{
  (e: 'submitted'): void;
}>();

const visible = defineModel<boolean>('visible', {
  default: false
});

const { formRef, validate, restoreValidation } = useNaiveForm();

type Model = Pick<Api.Rbac.UserEdit, 'userName' | 'name' | 'email' | 'isActive' | 'roleNames'> & {
  password?: string;
  confirmPassword?: string;
};

const model: Model = reactive(createDefaultModel());
const assignableRoles = ref<Api.Rbac.Role[]>([]);
const loadingRoles = ref(false);

const title = computed(() => (props.operateType === 'add' ? '新增用户' : '编辑用户'));

const rules = computed(() => {
  const { formRules, defaultRequiredRule, createConfirmPwdRule } = useFormRules();

  return {
    userName: defaultRequiredRule,
    name: defaultRequiredRule,
    email: formRules.email,
    ...(props.operateType === 'add'
      ? {
          password: formRules.pwd,
          confirmPassword: createConfirmPwdRule(model.password || '')
        }
      : {})
  };
});

const roleOptions = computed(() =>
  assignableRoles.value.map(role => ({
    label: role.name,
    value: role.name
  }))
);

function createDefaultModel(): Model {
  return {
    userName: '',
    name: '',
    email: '',
    isActive: true,
    roleNames: [],
    password: '',
    confirmPassword: ''
  };
}

async function fetchAssignableRoles() {
  loadingRoles.value = true;
  try {
    const { data, error } = await fetchGetAssignableRoles();
    if (!error) {
      assignableRoles.value = data.items || [];
    }
  } finally {
    loadingRoles.value = false;
  }
}

async function fetchUserRoles(userId: string) {
  loadingRoles.value = true;
  try {
    const { data, error } = await fetchGetUserRoles(userId);
    if (!error) {
      model.roleNames = (data.items || []).map(role => role.name);
    }
  } finally {
    loadingRoles.value = false;
  }
}

async function updateModel() {
  if (props.operateType === 'add') {
    Object.assign(model, createDefaultModel());
    return;
  }

  if (props.rowData) {
    Object.assign(model, {
      userName: props.rowData.userName,
      name: props.rowData.name || '',
      email: props.rowData.email,
      isActive: props.rowData.isActive,
      roleNames: []
    });
    await fetchUserRoles(props.rowData.id);
  }
}

function closeDrawer() {
  visible.value = false;
}

async function handleSubmit() {
  await validate();

  const submitData: Api.Rbac.UserEdit = {
    userName: model.userName,
    name: model.name,
    email: model.email,
    isActive: model.isActive,
    roleNames: model.roleNames || []
  };

  if (props.operateType === 'add') {
    submitData.password = model.password;
  }

  if (props.operateType === 'edit' && props.rowData) {
    submitData.concurrencyStamp = props.rowData.concurrencyStamp;
  }

  if (props.operateType === 'edit' && !props.rowData) {
    return;
  }

  const result =
    props.operateType === 'add' ? await fetchAddUser(submitData) : await fetchUpdateUser(props.rowData!.id, submitData);

  if (!result.error) {
    const userId = props.operateType === 'add' ? result.data?.id : props.rowData?.id;
    if (userId) {
      await fetchAssignRolesToUser(userId, { roleNames: model.roleNames || [] });
    }
  }

  if (!result.error) {
    window.$message?.success($t('common.updateSuccess'));
    closeDrawer();
    emit('submitted');
  }
}

watch(visible, () => {
  if (visible.value) {
    updateModel();
    restoreValidation();
    fetchAssignableRoles();
  }
});
</script>

<template>
  <NDrawer v-model:show="visible" display-directive="show" :width="460">
    <NDrawerContent :title="title" :native-scrollbar="false" closable>
      <NForm ref="formRef" :model="model" :rules="rules" label-placement="left" :label-width="96">
        <NFormItem label="用户名" path="userName">
          <NInput v-model:value="model.userName" placeholder="请输入用户名" />
        </NFormItem>
        <NFormItem label="姓名" path="name">
          <NInput v-model:value="model.name" placeholder="请输入姓名" />
        </NFormItem>
        <NFormItem label="邮箱" path="email">
          <NInput v-model:value="model.email" placeholder="请输入邮箱" />
        </NFormItem>
        <NFormItem label="角色" path="roleNames">
          <NSelect
            v-model:value="model.roleNames"
            multiple
            :options="roleOptions"
            :loading="loadingRoles"
            placeholder="请选择角色"
            clearable
          />
        </NFormItem>
        <NFormItem v-if="operateType === 'add'" label="密码" path="password">
          <NInput v-model:value="model.password" type="password" show-password-on="click" placeholder="请输入密码" />
        </NFormItem>
        <NFormItem v-if="operateType === 'add'" label="确认密码" path="confirmPassword">
          <NInput
            v-model:value="model.confirmPassword"
            type="password"
            show-password-on="click"
            placeholder="请再次输入密码"
          />
        </NFormItem>
        <NFormItem label="状态" path="isActive">
          <NSwitch v-model:value="model.isActive">
            <template #checked>启用</template>
            <template #unchecked>禁用</template>
          </NSwitch>
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
