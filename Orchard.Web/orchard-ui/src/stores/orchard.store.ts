import useNotifications from '@/components/notification/state';
import { axiosInstance, setToken } from '@/shared/axios-config';
import type { IFruit } from '@/shared/models/fruit.interface';
import type { ILoginResult } from '@/shared/models/login-result.inerface';
import type { User } from '@/shared/models/user.model';
import type { CreateAccountModel } from '@/views/account/models/create-account.model';
import { defineStore } from 'pinia';
import { ref } from 'vue';

export interface IOrchardStore {
  token?: string | null;
  refreshToken?: string | null;
  user?: User | null;
}

export const useOrchardStore = defineStore('orchardStore', () => {
  const notys = useNotifications();
  const user = ref<User | null>(null);
  const token = ref<string | null>(null);
  const refreshToken = ref<string | null>(null);
  const products = ref<IFruit[] | null>(null);

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
      token.value = response.token;
      refreshToken.value = response.refreshToken;
      user.value = {
        id: Number(response.claims.find((x) => x.type === 'userId')?.value),
        email: response.claims.find((x) => x.type === 'email')?.value,
        firstName: response.claims.find((x) => x.type === 'firstName')?.value,
        lastName: response.claims.find((x) => x.type === 'lastName')?.value,
        birthDate: response.claims.find((x) => x.type === 'birthDate')?.value
      } as User;

      setToken(token.value);

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

  async function getProducts(pageNumber: number, perPage: number): Promise<IFruit[]> {
    const fruit = (
      await axiosInstance.get<IFruit[]>(`api/fruit/page/${pageNumber}/size/${perPage}`)
    ).data;
    products.value = fruit;
    return fruit;
  }

  return { createAccount, logIn, user, token, refreshToken, getProducts };
});
