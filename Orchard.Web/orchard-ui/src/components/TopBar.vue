<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import { MenuItem } from './dropdown-menu/models/menu-item.model';
import DropdownMenu from './dropdown-menu/DropdownMenu.vue';
import { computed } from 'vue';
import type { CartItem } from '@/shared/cart/cart-item.model';
const orchardStore = useOrchardStore();

const menuItems: MenuItem[] = [
  {
    route: {
      path: '/account/settings'
    },
    text: 'Settings',
    icon: 'fa fa-gear'
  },
  {
    route: {
      path: '/account/orders'
    },
    text: 'Orders',
    icon: 'fa-solid fa-bag-shopping'
  },
  {
    route: {
      path: '/logout'
    },
    text: 'Log Out',
    icon: 'fa-solid fa-person-through-window'
  }
];

const cartItems = computed<CartItem[]>(() => {
  const fruit = orchardStore.products;
  const cartItems: CartItem[] = [];
  for (const key in orchardStore.cart) {
    const foundFruit = fruit?.find((x) => x.id === Number(key));
    if (
      Object.prototype.hasOwnProperty.call(orchardStore.cart, key) &&
      orchardStore.cart[key] > 0 &&
      foundFruit
    ) {
      cartItems.push({
        fruit: foundFruit,
        text: foundFruit.name,
        total: orchardStore.cart[key]
      } as CartItem);
    }
  }
  return cartItems;
});
</script>
<template>
  <div class="top-bar has-background-warning">
    <div
      class="p-3 is-flex is-flex-direction-row is-justify-content-space-between is-align-items-center"
    >
      <button class="button is-round is-warning is-ghost">
        <span class="icon">
          <i class="fa fa-cart-shopping"></i>
        </span>
      </button>
      <div class="">Hello {{ orchardStore.user?.firstName }}!</div>
      <DropdownMenu :menu-items="menuItems" :icon-class="'fa fa-user'" />
    </div>
  </div>
</template>
<style scoped>
.top-bar {
  width: 100%;
  min-height: 65px;
  border-radius: 65px 65px 65px 65px;
}
</style>
