import BasketsPage from '@/views/baskets/BasketsPage.vue';
import CreateAccountForm from '@/views/account/CreateAccountForm.vue';
import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useOrchardStore } from '@/stores/orchard.store';
import LogIn from '@/views/account/LogIn.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: BasketsPage,
      beforeEnter: () => {
        const orchardStore = useOrchardStore();
        if (!orchardStore.token) {
          return '/log-in';
        }
        return true;
      }
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
    }
  ] as RouteRecordRaw[]
});

export default router;
