import { abpRequest } from '../request/abp';

/** get user list */
export function fetchGetUserList(params?: Api.Rbac.UserSearchParams) {
  return abpRequest<Api.Rbac.UserList>({
    url: '/api/identity/users',
    method: 'get',
    params
  });
}

/** create user */
export function fetchAddUser(data: Api.Rbac.UserEdit) {
  return abpRequest<Api.Rbac.User>({
    url: '/api/identity/users',
    method: 'post',
    data
  });
}

/** update user */
export function fetchUpdateUser(id: string, data: Api.Rbac.UserEdit) {
  return abpRequest<Api.Rbac.User>({
    url: `/api/identity/users/${id}`,
    method: 'put',
    data
  });
}

/** delete user */
export function fetchDeleteUser(id: string) {
  return abpRequest<void>({
    url: `/api/identity/users/${id}`,
    method: 'delete'
  });
}

/** get user roles */
export function fetchGetUserRoles(id: string) {
  return abpRequest<Api.Rbac.RoleResult>({
    url: `/api/identity/users/${id}/roles`,
    method: 'get'
  });
}

/** get assignable roles */
export function fetchGetAssignableRoles() {
  return abpRequest<Api.Rbac.RoleResult>({
    url: '/api/identity/users/assignable-roles',
    method: 'get'
  });
}

/** assign roles to user */
export function fetchAssignRolesToUser(id: string, data: Api.Rbac.AssignRolesInput) {
  return abpRequest<void>({
    url: `/api/identity/users/${id}/roles`,
    method: 'put',
    data
  });
}

/** get role list */
export function fetchGetRoleList(params?: Api.Rbac.RoleSearchParams) {
  return abpRequest<Api.Rbac.RoleList>({
    url: '/api/identity/roles',
    method: 'get',
    params
  });
}

/** create role */
export function fetchAddRole(data: Api.Rbac.RoleEdit) {
  return abpRequest<Api.Rbac.Role>({
    url: '/api/identity/roles',
    method: 'post',
    data
  });
}

/** update role */
export function fetchUpdateRole(id: string, data: Api.Rbac.RoleEdit) {
  return abpRequest<Api.Rbac.Role>({
    url: `/api/identity/roles/${id}`,
    method: 'put',
    data
  });
}

/** delete role */
export function fetchDeleteRole(id: string) {
  return abpRequest<void>({
    url: `/api/identity/roles/${id}`,
    method: 'delete'
  });
}

/** get permissions */
export function fetchGetPermissions(providerName: string, providerKey: string) {
  return abpRequest<Api.Rbac.PermissionList>({
    url: '/api/permission-management/permissions',
    method: 'get',
    params: { providerName, providerKey }
  });
}

/** update permissions */
export function fetchUpdatePermissions(
  providerName: string,
  providerKey: string,
  data: Api.Rbac.UpdatePermissionsRequest
) {
  return abpRequest<void>({
    url: '/api/permission-management/permissions',
    method: 'put',
    params: { providerName, providerKey },
    data
  });
}
