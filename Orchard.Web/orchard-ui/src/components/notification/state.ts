import { ref } from 'vue';
import type { ToastNotification } from './toast-notification.interface';
import { v4 as uuidv4 } from 'uuid';

const notifications = ref<ToastNotification[]>([]);
export default function useNotifications() {
  const notify = (options: ToastNotification): string => {
    if (!options.id) {
      const id = uuidv4();
      options.id = id;
    }

    notifications.value.push(options);
    return options.id!;
  };

  const removeNotification = (id: string) => {
    const idx = notifications.value.findIndex((x) => x.id === id);
    if (idx > -1) {
      notifications.value.splice(idx, 1);
    }
  };

  return {
    notify,
    removeNotification,
    notifications
  };
}
