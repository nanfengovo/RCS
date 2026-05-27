import type { CustomAxiosRequestConfig } from '@sa/axios';
import { request as baseRequest } from './index';

export async function request<T>(config: CustomAxiosRequestConfig): Promise<T> {
  const { data, error } = await baseRequest<T>(config);

  if (error) {
    throw error;
  }

  return data as T;
}
