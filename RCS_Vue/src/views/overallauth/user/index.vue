<script setup lang="tsx">
import { reactive } from 'vue';
import { NButton, NCard, NDataTable, NPopconfirm, NTag } from 'naive-ui';
import type { FlatResponseData } from '@sa/axios';
import type { PaginationData } from '@sa/hooks';
import { rbacPermissions } from '@/constants/rbac';
import { fetchDeleteUser, fetchGetUserList, fetchGetUserRoles } from '@/service/api';
import { useAppStore } from '@/store/modules/app';
import { useNaivePaginatedTable, useTableOperate } from '@/hooks/common/table';
import { useAuth } from '@/hooks/business/auth';
import { $t } from '@/locales';
import TableHeaderOperation from '@/components/advanced/table-header-operation.vue';
import UserSearch from './modules/user-search.vue';
import UserOperateDrawer from './modules/user-operate-drawer.vue';

defineOptions({
  name: 'OverAllAuthUser'
});

const appStore = useAppStore();
const { hasAuth } = useAuth();
const permissions = rbacPermissions.identity.users;
const statusRecord: Record<Api.Common.EnableStatus, string> = {
  '1': $t('page.overallauth.user.statusEnabled'),
  '2': $t('page.overallauth.user.statusDisabled')
};

const searchParams: Api.Rbac.UserSearchParams = reactive({
  Filter: '',
  Sorting: '',
  SkipCount: 0,
  MaxResultCount: 10
});

const userRolesMap = reactive<Record<string, string[]>>({});

async function fetchUsersRoles(users: Api.Rbac.User[]) {
  const entries = await Promise.allSettled(
    users.map(async user => {
      const { data, error } = await fetchGetUserRoles(user.id);
      return [user.id, !error && data ? (data.items || []).map(role => role.name) : []] as const;
    })
  );

  entries.forEach(entry => {
    if (entry.status === 'fulfilled') {
      const [userId, roleNames] = entry.value;
      userRolesMap[userId] = roleNames;
    }
  });
}

async function getUsersWithRoles() {
  const response = await fetchGetUserList(searchParams);
  if (!response.error && response.data?.items) {
    await fetchUsersRoles(response.data.items);
  }
  return response;
}

function abpTransform(response: FlatResponseData<any, Api.Rbac.UserList>): PaginationData<Api.Rbac.User> {
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
  api: getUsersWithRoles,
  transform: abpTransform,
  onPaginationParamsChange: params => {
    searchParams.SkipCount = ((params.page ?? 1) - 1) * (params.pageSize ?? 10);
    searchParams.MaxResultCount = params.pageSize ?? 10;
  },
  columns: () => [
    {
      type: 'selection',
      align: 'center',
      width: 48
    },
    {
      key: 'index',
      title: $t('common.index'),
      align: 'center',
      width: 64,
      render: (_, index) => index + 1
    },
    {
      key: 'userName',
      title: $t('page.overallauth.user.userName'),
      align: 'center',
      minWidth: 140
    },
    {
      key: 'name',
      title: $t('page.overallauth.user.name'),
      align: 'center',
      minWidth: 120,
      render: row => row.name || '-'
    },
    {
      key: 'email',
      title: $t('page.overallauth.user.email'),
      align: 'center',
      minWidth: 180
    },
    {
      key: 'roleNames',
      title: $t('page.overallauth.user.roleNames'),
      align: 'center',
      minWidth: 180,
      render: row => {
        const roleNames = userRolesMap[row.id] || [];
        if (!roleNames.length) return '-';

        return (
          <div class="flex-center flex-wrap gap-4px">
            {roleNames.map(roleName => (
              <NTag key={roleName} type="info" size="small">
                {roleName}
              </NTag>
            ))}
          </div>
        );
      }
    },
    {
      key: 'isActive',
      title: $t('page.overallauth.user.status'),
      align: 'center',
      width: 100,
      render: row => {
        const status: Api.Common.EnableStatus = row.isActive ? '1' : '2';
        const tagMap: Record<Api.Common.EnableStatus, NaiveUI.ThemeColor> = {
          '1': 'success',
          '2': 'warning'
        };

        return <NTag type={tagMap[status]}>{statusRecord[status]}</NTag>;
      }
    },
    {
      key: 'creationTime',
      title: $t('page.overallauth.user.creationTime'),
      align: 'center',
      width: 180,
      render: row => (row.creationTime ? new Date(row.creationTime).toLocaleString('zh-CN') : '-')
    },
    {
      key: 'operate',
      title: $t('common.operate'),
      align: 'center',
      width: 140,
      render: row => (
        <div class="flex-center gap-8px">
          <NButton type="primary" ghost size="small" disabled={!hasAuth(permissions.update)} onClick={() => edit(row.id)}>
            {$t('common.edit')}
          </NButton>
          <NPopconfirm onPositiveClick={() => handleDelete(row.id)}>
            {{
              default: () => $t('common.confirmDelete'),
              trigger: () => (
                <NButton type="error" ghost size="small" disabled={!hasAuth(permissions.delete)}>
                  {$t('common.delete')}
                </NButton>
              )
            }}
          </NPopconfirm>
        </div>
      )
    }
  ]
});

const { drawerVisible, operateType, editingData, handleAdd, handleEdit, checkedRowKeys, onBatchDeleted, onDeleted } =
  useTableOperate(data, 'id', getData);

async function handleBatchDelete() {
  if (!checkedRowKeys.value.length) return;

  const results = await Promise.allSettled(checkedRowKeys.value.map(id => fetchDeleteUser(id)));
  const failedCount = results.filter(result => result.status === 'rejected').length;

  if (failedCount) {
    window.$message?.warning(`成功删除 ${results.length - failedCount} 个用户，失败 ${failedCount} 个`);
  }

  await onBatchDeleted();
}

async function handleDelete(id: string) {
  const { error } = await fetchDeleteUser(id);
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
    <UserSearch v-model:model="searchParams" @search="getDataByPage" />
    <NCard :title="$t('page.overallauth.user.title')" :bordered="false" size="small" class="card-wrapper sm:flex-1-hidden">
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
        :scroll-x="1110"
        :loading="loading"
        remote
        :row-key="row => row.id"
        :pagination="mobilePagination"
        class="sm:h-full"
      />
      <UserOperateDrawer
        v-model:visible="drawerVisible"
        :operate-type="operateType"
        :row-data="editingData"
        @submitted="getDataByPage"
      />
    </NCard>
  </div>
</template>
