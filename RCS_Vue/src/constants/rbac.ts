export const rbacPermissions = {
  identity: {
    users: {
      default: 'AbpIdentity.Users',
      create: 'AbpIdentity.Users.Create',
      update: 'AbpIdentity.Users.Update',
      delete: 'AbpIdentity.Users.Delete'
    },
    roles: {
      default: 'AbpIdentity.Roles',
      create: 'AbpIdentity.Roles.Create',
      update: 'AbpIdentity.Roles.Update',
      delete: 'AbpIdentity.Roles.Delete'
    }
  },
  wms: {
    location: {
      default: 'RCS.Wms.Location',
      create: 'RCS.Wms.Location.Create',
      update: 'RCS.Wms.Location.Update',
      delete: 'RCS.Wms.Location.Delete',
      active: 'RCS.Wms.Location.Active'
    }
  },
  dispatch: {
    task: {
      default: 'RCS.Dispatch.Task',
      retry: 'RCS.Dispatch.Task.Retry',
      skip: 'RCS.Dispatch.Task.Skip',
      cancel: 'RCS.Dispatch.Task.Cancel'
    }
  },
  device: {
    connection: {
      default: 'RCS.Device.Connection',
      create: 'RCS.Device.Connection.Create',
      update: 'RCS.Device.Connection.Update',
      delete: 'RCS.Device.Connection.Delete',
      enable: 'RCS.Device.Connection.Enable',
      disable: 'RCS.Device.Connection.Disable'
    }
  },
  diagnostics: {
    logs: {
      default: 'RCS.Diagnostics.Logs',
      delete: 'RCS.Diagnostics.Logs.Delete'
    }
  }
} as const;

export type RbacPermissionMap = typeof rbacPermissions;
