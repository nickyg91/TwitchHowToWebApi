import type { OrderStatus } from '../enums/order-status.enum';
import type { IBasketFruit } from './basket-fruit.interface';

export interface IBasketModel {
  id?: number | null;
  orderStatus: OrderStatus | null;
  fruit: IBasketFruit[] | null;
  totalFruit: number;
}
