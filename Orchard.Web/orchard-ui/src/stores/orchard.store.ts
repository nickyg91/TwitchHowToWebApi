import { axiosInstance } from '@/shared/axios-config';
import type { ILoginResult } from '@/shared/models/login-result.inerface';
import type { User } from '@/shared/models/user.model';
import type { CreateAccountModel } from '@/views/account/models/create-account.model';
import { defineStore } from 'pinia';
import { reactive } from 'vue';

export interface IOrchardStore {
  token?: string | null;
  refreshToken?: string | null;
  user?: User | null;
}

export const useOrchardStore = defineStore('orchardStore', () => {
  const orchardState = reactive({
    token: null,
    refreshToken: null,
    user: null
  } as IOrchardStore);

  async function createAccount(account: CreateAccountModel): Promise<boolean> {
    return (await axiosInstance.post<boolean>('/api/account/create', account)).data;
  }

  async function logIn(email: string, password: string): Promise<ILoginResult | null> {
    const response = (
      await axiosInstance.post<ILoginResult>('/api/account/login', {
        email,
        password
      })
    ).data;
    return response;
  }

  return { createAccount, logIn, orchardState };
});
