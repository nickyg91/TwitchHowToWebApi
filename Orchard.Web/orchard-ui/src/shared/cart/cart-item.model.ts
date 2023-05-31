import { MenuItem } from '@/components/dropdown-menu/models/menu-item.model';
import type { IFruit } from '../models/fruit.interface';

export class CartItem extends MenuItem {
  fruit!: IFruit;
  total!: number;
}
