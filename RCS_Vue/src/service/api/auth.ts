import { abpRequest } from '../request/abp';

function createTokenForm(data: Record<string, string>) {
  return new URLSearchParams(data);
}

/**
 * Login
 *
 * @param userName User name
 * @param password Password
 */
export function fetchLogin(userName: string, password: string) {
  return abpRequest<Api.Auth.LoginToken>({
    url: '/connect/token',
    method: 'post',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    data: createTokenForm({
      grant_type: 'password',
      client_id: import.meta.env.VITE_ABP_CLIENT_ID,
      username: userName,
      password,
      scope: import.meta.env.VITE_ABP_SCOPE
    })
  });
}

/** Get user info */
export function fetchGetUserInfo() {
  return abpRequest<Api.Abp.ApplicationConfiguration>({ url: '/api/abp/application-configuration' });
}

/**
 * Refresh token
 *
 * @param refreshToken Refresh token
 */
export function fetchRefreshToken(refreshToken: string) {
  return abpRequest<Api.Auth.LoginToken>({
    url: '/connect/token',
    method: 'post',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded'
    },
    data: createTokenForm({
      grant_type: 'refresh_token',
      client_id: import.meta.env.VITE_ABP_CLIENT_ID,
      refresh_token: refreshToken
    })
  });
}

/**
 * return custom backend error
 *
 * @param code error code
 * @param msg error message
 */
export function fetchCustomBackendError(code: string, msg: string) {
  return abpRequest({ url: '/auth/error', params: { code, msg } });
}
