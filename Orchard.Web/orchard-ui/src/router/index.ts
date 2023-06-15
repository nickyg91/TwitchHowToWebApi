import CreateAccountForm from '@/views/account/CreateAccountForm.vue';
import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useOrchardStore } from '@/stores/orchard.store';
import LogIn from '@/views/account/LogIn.vue';
import FruitList from '@/views/fruit/fruit-list/FruitList.vue';
import AccountOrders from '@/views/account/AccountOrders.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: FruitList,
      beforeEnter: authentictedGuard
    },
    {
      path: '/log-in',
      name: 'logIn',
      component: LogIn
    },
    {
      path: '/account/create',
      name: 'createAccount',
      component: CreateAccountForm
    },
    {
      path: '/orchard',
      name: 'orchard',
      component: FruitList,
      beforeEnter: authentictedGuard
    },
    {
      path: '/account/orders',
      name: 'orders',
      component: AccountOrders,
      beforeEnter: authentictedGuard
    }
  ] as RouteRecordRaw[]
});

function authentictedGuard(): boolean | string {
  const orchardStore = useOrchardStore();
  if (!orchardStore.token) {
    return '/log-in';
  }
  return true;
}

export default router;
