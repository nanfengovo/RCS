import type { CustomAxiosRequestConfig } from '@sa/axios';
import { request as baseRequest } from './abp';

export async function request<T>(config: CustomAxiosRequestConfig): Promise<T> {
  return baseRequest<T>(config);
}
