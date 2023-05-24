import useNotifications from '@/components/notification/state';
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
  const notys = useNotifications();
  const orchardState = reactive({
    token: null,
    refreshToken: null,
    user: null
  } as IOrchardStore);

  async function createAccount(account: CreateAccountModel): Promise<boolean> {
    try {
      const resp = (await axiosInstance.post<boolean>('/api/account/create', account)).data;

      if (resp) {
        notys.notify({
          autoClose: false,
          duration: 2,
          isLight: true,
          message: 'Account created successfully!',
          title: 'Account Created',
          type: 'success'
        });
        return resp;
      } else {
        notys.notify({
          autoClose: false,
          duration: 2,
          isLight: true,
          message: 'Error: Failed to create account!',
          title: 'Error',
          type: 'danger'
        });
        return false;
      }
    } catch (exception) {
      notys.notify({
        autoClose: false,
        duration: 2,
        isLight: true,
        message: 'Error: Failed to create account!',
        title: 'Error',
        type: 'danger'
      });
      return false;
    }
  }

  async function logIn(email: string, password: string): Promise<ILoginResult | null> {
    try {
      const response = (
        await axiosInstance.post<ILoginResult>('/api/account/login', {
          email,
          password
        })
      ).data;
      orchardState.token = response.token;
      orchardState.refreshToken = response.refreshToken;
      return response;
    } catch (error) {
      notys.notify({
        autoClose: true,
        duration: 5,
        message: 'Error logging in.',
        title: 'Unable to log in.',
        isLight: false,
        type: 'danger'
      });
      return null;
    }
  }

  return { createAccount, logIn, orchardState };
});
