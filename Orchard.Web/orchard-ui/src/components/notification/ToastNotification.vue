<script setup lang="ts">
import { ref } from 'vue';
import type { ToastNotification } from './toast-notification.interface';
import { onMounted } from 'vue';
import { computed } from 'vue';
const props = defineProps<ToastNotification>();
const emit = defineEmits<{
  (e: 'close'): void;
}>();
const timer = ref(-1);
const delay = ref<number>(0);
const startedAt = ref<number>(0);

const close = () => {
  emit('close');
};

onMounted(() => {
  if (props.autoClose) {
    startedAt.value = Date.now();
    delay.value = props.duration * 1000;
    timer.value = setTimeout(close, delay.value);
  }
});

const title = computed(() => {
  return props.title ? props.title : 'Notification';
});

const message = computed(() => {
  return props.message;
});

const color = computed(() => {
  switch (props.type) {
    case 'info':
      return 'is-info';
    case 'warning':
      return 'is-warning';
    case 'success':
      return 'is-success';
    case 'danger':
      return 'is-danger';
    case 'link':
      return 'is-link';
    case 'primary':
      return 'is-primary';
    default:
      return 'is-primary';
  }
});

const isLight = computed(() => {
  return props.isLight ? 'is-light' : '';
});

const notyStyle = computed(() => `notification ${isLight.value} ${color.value}`);
</script>
<template>
  <div @click="close" :class="notyStyle">
    <button @click="close" class="delete"></button>
    <span class="is-size-4">
      {{ title }}
    </span>
    <div>
      {{ message }}
    </div>
  </div>
</template>

<style scoped>
.notification {
  z-index: 1;
  width: 350px;
  position: relative;
}
</style>
