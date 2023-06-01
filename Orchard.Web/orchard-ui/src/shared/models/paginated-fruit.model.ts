import type { IFruit } from './fruit.interface';

export class PaginatedFruit {
  fruit!: IFruit[];
  totalFruit!: number;
  totalPages!: number;
}
