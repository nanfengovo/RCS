<script setup lang="tsx">
import { reactive } from 'vue';
import { NButton, NCard, NDataTable, NPopconfirm, NSpace, NTag } from 'naive-ui';
import type { FlatResponseData } from '@sa/axios';
import type { PaginationData } from '@sa/hooks';
import { rbacPermissions } from '@/constants/rbac';
import { fetchDeleteRole, fetchGetPermissions, fetchGetRoleList } from '@/service/api';
import { useAppStore } from '@/store/modules/app';
import { useNaivePaginatedTable, useTableOperate } from '@/hooks/common/table';
import { useAuth } from '@/hooks/business/auth';
import { $t } from '@/locales';
import TableHeaderOperation from '@/components/advanced/table-header-operation.vue';
import RoleSearch from './modules/role-search.vue';
import RoleOperateDrawer from './modules/role-operate-drawer.vue';

defineOptions({
  name: 'OverAllAuthRole'
});

const appStore = useAppStore();
const { hasAuth } = useAuth();
const permissions = rbacPermissions.identity.roles;

const searchParams: Api.Rbac.RoleSearchParams = reactive({
  Filter: '',
  Sorting: '',
  SkipCount: 0,
  MaxResultCount: 10
});

const rolePermissionsMap = reactive<Record<string, string[]>>({});

async function fetchRolesPermissions(roles: Api.Rbac.Role[]) {
  const entries = await Promise.allSettled(
    roles.map(async role => {
      const { data, error } = await fetchGetPermissions('R', role.name);
      const granted =
        !error && data
          ? data.groups.flatMap(group =>
              group.permissions
                .filter(permission => permission.isGranted)
                .map(permission => permission.displayName || permission.name)
            )
          : [];

      return [role.id, granted] as const;
    })
  );

  entries.forEach(entry => {
    if (entry.status === 'fulfilled') {
      const [roleId, granted] = entry.value;
      rolePermissionsMap[roleId] = granted;
    }
  });
}

async function getRolesWithPermissions() {
  const response = await fetchGetRoleList(searchParams);
  if (!response.error && response.data?.items) {
    await fetchRolesPermissions(response.data.items);
  }
  return response;
}

function abpTransform(response: FlatResponseData<any, Api.Rbac.RoleList>): PaginationData<Api.Rbac.Role> {
  const { data, error } = response;

  if (!error && data) {
    return {
      data: data.items || [],
      pageNum: (searchParams.SkipCount || 0) / (searchParams.MaxResultCount || 10) + 1,
      pageSize: searchParams.MaxResultCount || 10,
      total: data.totalCount || 0
    };
  }

  return {
    data: [],
    pageNum: 1,
    pageSize: 10,
    total: 0
  };
}

const { columns, columnChecks, data, getData, getDataByPage, loading, mobilePagination } = useNaivePaginatedTable({
  api: getRolesWithPermissions,
  transform: abpTransform,
  onPaginationParamsChange: params => {
    searchParams.SkipCount = ((params.page ?? 1) - 1) * (params.pageSize ?? 10);
    searchParams.MaxResultCount = params.pageSize ?? 10;
  },
  columns: () => [
    {
      type: 'selection',
      align: 'center',
      width: 48,
      disabled: row => row.isStatic
    },
    {
      key: 'index',
      title: $t('common.index'),
      align: 'center',
      width: 64,
      render: (_, index) => index + 1
    },
    {
      key: 'name',
      title: '角色名称',
      align: 'center',
      minWidth: 140
    },
    {
      key: 'permissions',
      title: '拥有权限',
      align: 'center',
      minWidth: 320,
      render: row => {
        const granted = rolePermissionsMap[row.id] || [];
        if (!granted.length) return '-';

        return (
          <div class="flex-center flex-wrap gap-4px">
            {granted.slice(0, 8).map(permission => (
              <NTag key={permission} type="success" size="small">
                {permission}
              </NTag>
            ))}
            {granted.length > 8 ? <NTag size="small">+{granted.length - 8}</NTag> : null}
          </div>
        );
      }
    },
    {
      key: 'isDefault',
      title: '默认角色',
      align: 'center',
      width: 100,
      render: row => <NTag type={row.isDefault ? 'success' : 'default'}>{row.isDefault ? '是' : '否'}</NTag>
    },
    {
      key: 'isPublic',
      title: '公开角色',
      align: 'center',
      width: 100,
      render: row => <NTag type={row.isPublic ? 'info' : 'default'}>{row.isPublic ? '是' : '否'}</NTag>
    },
    {
      key: 'isStatic',
      title: '系统角色',
      align: 'center',
      width: 100,
      render: row => <NTag type={row.isStatic ? 'warning' : 'default'}>{row.isStatic ? '是' : '否'}</NTag>
    },
    {
      key: 'operate',
      title: $t('common.operate'),
      align: 'center',
      width: 140,
      render: row => (
        <NSpace justify="center">
          <NButton type="primary" ghost size="small" disabled={!hasAuth(permissions.update)} onClick={() => edit(row.id)}>
            {$t('common.edit')}
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDelete(row.id)}>
            {{
              default: () => $t('common.confirmDelete'),
              trigger: () => (
                <NButton type="error" ghost size="small" disabled={row.isStatic || !hasAuth(permissions.delete)}>
                  {$t('common.delete')}
                </NButton>
              )
            }}
          </NPopconfirm>
        </NSpace>
      )
    }
  ]
});

const { drawerVisible, operateType, editingData, handleAdd, handleEdit, checkedRowKeys, onBatchDeleted, onDeleted } =
  useTableOperate(data, 'id', getData);

async function handleBatchDelete() {
  if (!checkedRowKeys.value.length) return;

  const results = await Promise.allSettled(checkedRowKeys.value.map(id => fetchDeleteRole(id)));
  const failedCount = results.filter(result => result.status === 'rejected').length;

  if (failedCount) {
    window.$message?.warning(`成功删除 ${results.length - failedCount} 个角色，失败 ${failedCount} 个`);
  }

  await onBatchDeleted();
}

async function handleDelete(id: string) {
  const { error } = await fetchDeleteRole(id);
  if (!error) {
    await onDeleted();
  }
}

function edit(id: string) {
  handleEdit(id);
}
</script>

<template>
  <div class="min-h-500px flex-col-stretch gap-16px overflow-hidden lt-sm:overflow-auto">
    <RoleSearch v-model:model="searchParams" @search="getDataByPage" />
    <NCard title="角色管理" :bordered="false" size="small" class="card-wrapper sm:flex-1-hidden">
      <template #header-extra>
        <TableHeaderOperation
          v-model:columns="columnChecks"
          :disabled-add="!hasAuth(permissions.create)"
          :disabled-delete="checkedRowKeys.length === 0 || !hasAuth(permissions.delete)"
          :loading="loading"
          @add="handleAdd"
          @delete="handleBatchDelete"
          @refresh="getData"
        />
      </template>
      <NDataTable
        v-model:checked-row-keys="checkedRowKeys"
        :columns="columns"
        :data="data"
        size="small"
        :flex-height="!appStore.isMobile"
        :scroll-x="1012"
        :loading="loading"
        remote
        :row-key="row => row.id"
        :pagination="mobilePagination"
        class="sm:h-full"
      />
      <RoleOperateDrawer
        v-model:visible="drawerVisible"
        :operate-type="operateType"
        :row-data="editingData"
        @submitted="getDataByPage"
      />
    </NCard>
  </div>
</template>
