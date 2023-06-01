<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import { MenuItem } from './dropdown-menu/models/menu-item.model';
import DropdownMenu from './dropdown-menu/DropdownMenu.vue';
import useModal from './modal/modal.store';
import CartContents from './cart/CartContents.vue';
import SearchBar from './SearchBar.vue';
const orchardStore = useOrchardStore();
const modal = useModal();

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

function showCartModal(): void {
  modal.open(CartContents, 'Cart', null);
}
</script>
<template>
  <div class="top-bar has-background-warning">
    <div
      class="p-3 is-flex is-flex-direction-row is-flex-shrink-2 is-justify-content-space-between is-align-items-center"
    >
      <button @click="showCartModal" class="button is-round is-warning is-ghost">
        <span class="icon">
          <i class="fa fa-cart-shopping"></i>
        </span>
      </button>
      <SearchBar />
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
