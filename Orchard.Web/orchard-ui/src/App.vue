<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/DialogBox.vue';
import useNotifications from './components/notification/state';
import ToastNotification from './components/notification/ToastNotification.vue';
const notys = useNotifications();
</script>
<template>
  <section class="section">
    <div class="container">
      <RouterView class="card" />
    </div>
    <DialogBox />
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
  </section>
</template>
<style lang="scss" scoped>
.toast-notifications {
  z-index: 100;
  position: absolute;
  top: 0.5rem;
  right: 0.5rem;
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
