import { rbacPermissions } from '@/constants/rbac';

export interface FrontendModule {
  key: 'wms' | 'dispatch' | 'device' | 'diagnostics' | 'identity';
  title: string;
  routePrefix: string;
  permissions: Record<string, unknown>;
}

export const frontendModules: FrontendModule[] = [
  {
    key: 'wms',
    title: 'WMS',
    routePrefix: '/wms',
    permissions: rbacPermissions.wms
  },
  {
    key: 'dispatch',
    title: 'Dispatch',
    routePrefix: '/dispatch',
    permissions: rbacPermissions.dispatch
  },
  {
    key: 'device',
    title: 'Device',
    routePrefix: '/device',
    permissions: rbacPermissions.device
  },
  {
    key: 'diagnostics',
    title: 'Diagnostics',
    routePrefix: '/diagnostics',
    permissions: rbacPermissions.diagnostics
  },
  {
    key: 'identity',
    title: 'Identity',
    routePrefix: '/identity',
    permissions: rbacPermissions.identity
  }
];

export function getFrontendModule(key: FrontendModule['key']) {
  return frontendModules.find(item => item.key === key);
}
