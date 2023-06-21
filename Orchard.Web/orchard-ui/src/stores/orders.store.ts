import useNotifications from '@/components/notification/state';
import { axiosInstance } from '@/shared/axios-config';
import type { IBasketModel } from '@/shared/interfaces/basket.interface';
import { defineStore } from 'pinia';
import { reactive } from 'vue';

export interface IOrchardState {
  orders: IBasketModel[];
}
export const useOrderStore = defineStore('ordersStore', () => {
  const state = reactive({} as IOrchardState);
  const noty = useNotifications();

  async function getOrders(): Promise<void> {
    try {
      state.orders = (await axiosInstance.get('api/orders')).data;
    } catch (error) {
      noty.notify({
        autoClose: true,
        message: 'Could not get your orders!',
        title: 'Error',
        type: 'danger'
      });
    }
  }

  return { state, getOrders };
});
