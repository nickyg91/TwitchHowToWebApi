import type { RouteLocationRaw } from 'vue-router';

export abstract class MenuItem {
  text!: string;
  route!: RouteLocationRaw;
  icon?: string | null;
}
