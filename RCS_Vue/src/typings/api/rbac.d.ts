declare namespace Api {
  namespace Rbac {
    interface PagedResult<T> {
      items: T[];
      totalCount: number;
    }

    interface PagedSearchParams {
      Filter?: string;
      Sorting?: string;
      SkipCount?: number;
      MaxResultCount?: number;
    }

    type UserSearchParams = PagedSearchParams;

    interface User {
      id: string;
      tenantId?: string | null;
      userName: string;
      name?: string | null;
      surname?: string | null;
      email: string;
      emailConfirmed?: boolean;
      phoneNumber?: string | null;
      phoneNumberConfirmed?: boolean;
      isActive: boolean;
      lockoutEnabled?: boolean;
      accessFailedCount?: number;
      lockoutEnd?: string | null;
      concurrencyStamp?: string;
      creationTime?: string;
      lastModificationTime?: string | null;
      extraProperties?: Record<string, unknown>;
    }

    type UserList = PagedResult<User>;

    interface UserEdit {
      userName: string;
      name?: string | null;
      surname?: string | null;
      email: string;
      phoneNumber?: string | null;
      password?: string;
      isActive: boolean;
      lockoutEnabled?: boolean;
      roleNames?: string[];
      concurrencyStamp?: string;
      extraProperties?: Record<string, unknown>;
    }

    type RoleSearchParams = PagedSearchParams;

    interface Role {
      id: string;
      tenantId?: string | null;
      name: string;
      isDefault: boolean;
      isStatic: boolean;
      isPublic: boolean;
      concurrencyStamp?: string;
      creationTime?: string;
      lastModificationTime?: string | null;
      extraProperties?: Record<string, unknown>;
    }

    type RoleList = PagedResult<Role>;

    interface RoleResult {
      items: Role[];
    }

    interface AssignRolesInput {
      roleNames: string[];
    }

    interface RoleEdit {
      name: string;
      isDefault: boolean;
      isPublic: boolean;
      concurrencyStamp?: string;
      extraProperties?: Record<string, unknown>;
    }

    interface PermissionGrantInfo {
      name: string;
      displayName: string;
      parentName: string | null;
      isGranted: boolean;
      allowedProviders: string[];
      grantedProviders: { providerName: string; providerKey: string }[];
    }

    interface PermissionGroup {
      name: string;
      displayName: string;
      displayNameKey?: string;
      displayNameResource?: string;
      permissions: PermissionGrantInfo[];
    }

    interface PermissionList {
      entityDisplayName: string;
      groups: PermissionGroup[];
    }

    interface PermissionUpdateItem {
      name: string;
      isGranted: boolean;
    }

    interface UpdatePermissionsRequest {
      permissions: PermissionUpdateItem[];
    }
  }
}
