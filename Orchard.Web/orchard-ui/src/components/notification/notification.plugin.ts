import type { App } from 'vue';
import ToastNotification from './ToastNotification.vue';
export * as noty from './state';

const toaster = {
  install(app: App) {
    app.component('ToastNotification', ToastNotification);
  }
};

export default toaster;
