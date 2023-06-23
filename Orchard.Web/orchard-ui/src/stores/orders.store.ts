import useNotifications from '@/components/notification/state';
import { axiosInstance } from '@/shared/axios-config';
import type { IBasketFruit } from '@/shared/interfaces/basket-fruit.interface';
import type { IBasketModel } from '@/shared/interfaces/basket.interface';
import { defineStore } from 'pinia';
import { reactive } from 'vue';

export interface IOrchardState {
  orders: IBasketModel[];
  orderDetails: Map<number, IBasketFruit[]>;
}
export const useOrderStore = defineStore('ordersStore', () => {
  const state = reactive({
    orders: [],
    orderDetails: new Map<number, IBasketFruit[]>()
  } as IOrchardState);
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

  async function getOrderDetails(id: number): Promise<void> {
    try {
      if (!state.orderDetails.has(id)) {
        const details = (await axiosInstance.get<IBasketFruit[]>(`api/orders/${id}/details`)).data;
        state.orderDetails.set(id, details);
      }
    } catch (error) {
      noty.notify({
        autoClose: true,
        message: 'Could not get details for this order!',
        title: 'Error',
        type: 'danger'
      });
    }
  }

  return { state, getOrders, getOrderDetails };
});
