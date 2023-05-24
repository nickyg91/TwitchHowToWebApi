<script setup lang="ts">
import useNotifications from './state';
import ToastNotification from './ToastNotification.vue';
const notys = useNotifications();
</script>
<template>
  <transition-group name="toast-notification" tag="div" class="toast-notifications">
    <ToastNotification
      v-for="noty in notys.notifications.value"
      :key="noty.id!"
      :id="noty.id"
      :auto-close="noty.autoClose"
      :is-light="noty.isLight"
      :message="noty.message"
      :duration="noty.duration"
      :type="noty.type"
      :title="noty.title"
      @close="
          () => {
            notys.removeNotification(noty.id!);
          }
        "
    />
  </transition-group>
</template>
<style scoped>
.toast-notifications {
  z-index: 100;
  position: absolute;
  top: 1.5rem;
  right: 1.5rem;
  overflow: hidden;
  height: 100%;
}
.toast-notification-enter-active {
  transition: opacity 0.5s ease;
}
.toast-notification-leave-active {
  transition: opacity 0.5s ease;
}

.toast-notification-enter-from,
.toast-notification-leave-to {
  opacity: 0;
}
</style>
