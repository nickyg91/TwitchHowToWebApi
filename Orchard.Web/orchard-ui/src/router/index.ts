import BasketsPage from '@/views/baskets/BasketsPage.vue';
import LoginForm from '@/views/account/LoginForm.vue';
import { createRouter, createWebHistory } from 'vue-router';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: BasketsPage
    },
    {
      path: '/account/create',
      name: 'createAccount',
      component: LoginForm
    }
  ]
});

export default router;
