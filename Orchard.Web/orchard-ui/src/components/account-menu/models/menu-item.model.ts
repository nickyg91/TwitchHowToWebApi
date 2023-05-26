import type { RouteLocationRaw } from 'vue-router';

export class MenuItem {
  text!: string;
  route!: RouteLocationRaw;
  icon?: string | null;
}
