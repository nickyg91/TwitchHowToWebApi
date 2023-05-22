import { ToastNotification } from './ToastNotification.vue';

const plugin: Plugin = {
  install(app) {
    app.component('ToastNotification', ToastNotification);
  }
};

export default plugin;
