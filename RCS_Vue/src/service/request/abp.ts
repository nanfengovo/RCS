import type { AxiosResponse } from 'axios';
import type { CustomAxiosRequestConfig } from '@sa/axios';
import { BACKEND_ERROR_CODE, createFlatRequest } from '@sa/axios';
import { useAuthStore } from '@/store/modules/auth';
import { localStg } from '@/utils/storage';
import { getServiceBaseURL } from '@/utils/service';
import { getAuthorization, handleExpiredRequest, showErrorMsg } from './shared';
import type { RequestInstanceState } from './type';

const isHttpProxy = import.meta.env.DEV && import.meta.env.VITE_HTTP_PROXY === 'Y';
const { baseURL } = getServiceBaseURL(import.meta.env, isHttpProxy);

function getAbpErrorMessage(data: unknown, fallback: string) {
  const response = data as Partial<Api.Abp.ErrorResponse | Api.Abp.OpenIddictErrorResponse> | undefined;

  if (response && 'error_description' in response && response.error_description) {
    return response.error_description;
  }

  const error = response && 'error' in response ? response.error : null;

  if (error && typeof error === 'object' && 'message' in error && error.message) {
    const validationErrors = error.validationErrors || [];
    const validationMessage = validationErrors
      .map((item: Api.Abp.ValidationError) => item.message)
      .filter(Boolean)
      .join('\n');

    return validationMessage || error.message;
  }

  return fallback;
}

function isConnectTokenRequest(url?: string) {
  return Boolean(url?.includes('/connect/token'));
}

export const abpRequest = createFlatRequest<unknown, unknown, RequestInstanceState>(
  {
    baseURL,
    validateStatus: status => status < 500
  },
  {
    defaultState: {
      errMsgStack: [],
      refreshTokenPromise: null
    },
    transform(response: AxiosResponse) {
      return response.data;
    },
    async onRequest(config) {
      const Authorization = getAuthorization();
      const lang = localStg.get('lang') || 'zh-CN';
      const tenant = localStg.get('tenant');

      Object.assign(config.headers, {
        'Accept-Language': lang,
        Authorization
      });

      if (tenant) {
        Object.assign(config.headers, { __tenant: tenant });
      }

      return config;
    },
    isBackendSuccess(response) {
      return response.status >= 200 && response.status < 300;
    },
    async onBackendFail(response, instance) {
      const authStore = useAuthStore();

      if (response.status === 401 && !isConnectTokenRequest(response.config.url)) {
        const success = await handleExpiredRequest(abpRequest.state);

        if (success) {
          const Authorization = getAuthorization();
          Object.assign(response.config.headers, { Authorization });

          return instance.request(response.config) as Promise<AxiosResponse>;
        }

        authStore.resetStore();
      }

      if (response.status === 403) {
        window.$message?.error('无权限访问该资源');
      }

      return null;
    },
    onError(error) {
      let message = error.message;

      if (error.code === BACKEND_ERROR_CODE || error.response?.data) {
        message = getAbpErrorMessage(error.response?.data, message);
      }

      showErrorMsg(abpRequest.state, message);
    }
  }
);

export async function request<T>(config: CustomAxiosRequestConfig) {
  const { data, error } = await abpRequest<T>(config);

  if (error) {
    throw error;
  }

  return data as T;
}
