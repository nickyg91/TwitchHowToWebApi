import { axiosInstance } from '@/shared/axios-config';
import type { User } from '@/shared/models/user.model';
import type { CreateAccountModel } from '@/views/account/models/create-account.model';
import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useOrchardStore = defineStore('orchardStore', () => {
  //const user: User | null = ref(null);

  async function createAccount(account: CreateAccountModel): Promise<boolean> {
    return (await axiosInstance.post<boolean>('/api/account/create', account)).data;
  }

  return { createAccount };
});
