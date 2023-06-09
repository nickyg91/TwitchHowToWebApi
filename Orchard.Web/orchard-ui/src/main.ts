import { createApp } from 'vue';
import { createPinia } from 'pinia';
import { plugin, defaultConfig } from '@formkit/vue';
import App from './App.vue';
import router from './router';

import './assets/sass/overrides/bulma-overrides.scss';
import './assets/sass/main.scss';
import '../node_modules/@fortawesome/fontawesome-free/scss/fontawesome.scss';
import '../node_modules/@fortawesome/fontawesome-free/scss/brands.scss';
import '../node_modules/@fortawesome/fontawesome-free/scss/solid.scss';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(
  plugin,
  defaultConfig({
    config: {
      classes: {
        input: 'input',
        outer: 'field',
        wrapper: 'control',
        label: 'label',
        help: 'has-text-info'
      }
    }
  })
);

app.mount('#app');
