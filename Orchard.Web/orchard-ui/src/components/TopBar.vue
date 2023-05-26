<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import type { MenuItem } from './account-menu/models/menu-item.model';
import AccountMenu from './account-menu/AccountMenu.vue';
import { ref } from 'vue';

const orchardStore = useOrchardStore();
const showAccountMenu = ref(false);

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

function onUserButtonClicked(): void {
  showAccountMenu.value = !showAccountMenu.value;
}

function onUserButtonBlurred(): void {
  showAccountMenu.value = false;
}
</script>
<template>
  <div class="top-bar has-background-warning">
    <div
      class="p-3 is-flex is-flex-direction-row is-justify-content-space-between is-align-items-center"
    >
      <div>
        <button class="button is-round is-warning is-ghost">
          <span class="icon">
            <i class="fa fa-cart-shopping"></i>
          </span>
        </button>
      </div>
      <div class="">Hello {{ orchardStore.user?.firstName }}!</div>
      <div class="dropdown" :class="{ 'is-active': showAccountMenu }">
        <div class="dropdown-trigger">
          <button
            @blur="onUserButtonBlurred"
            @click="onUserButtonClicked"
            class="button is-round is-warning is-ghost"
          >
            <span class="icon">
              <i class="fa fa-user"></i>
            </span>
          </button>
        </div>
        <div class="dropdown-menu">
          <div class="dropdown-content">
            <AccountMenu :menu-items="menuItems" />
          </div>
        </div>
      </div>
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
