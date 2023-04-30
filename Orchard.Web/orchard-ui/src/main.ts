import { createApp } from 'vue';
import { createPinia } from 'pinia';

import App from './App.vue';
import router from './router';

import './assets/sass/overrides/bulma-overrides.scss';
import './assets/sass/overrides/nord-dark.scss';
import './assets/sass/overrides/nord-light.scss';
import './assets/sass/main.scss';

const app = createApp(App);

app.use(createPinia());
app.use(router);

app.mount('#app');
