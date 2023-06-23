<script setup lang="ts">
defineProps<{ loading: boolean }>();
</script>
<template>
  <div
    v-if="loading"
    :class="{ 'show-loader': loading, 'hide-loader': !loading }"
    class="loader translucent-background"
  >
    <div class="has-text-centered">
      <i class="spinner fa fa-spinner fa-spin fa-3x"></i>
    </div>
  </div>
</template>
<style scoped>
@keyframes fadeIn {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}

@keyframes fadeOut {
  0% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}

.spinner {
  top: 50%;
}

.show-loader {
  animation: fadeIn ease 2s;
}

.hide-loader {
  animation: fadeOut ease 2s;
  animation-fill-mode: forwards;
}
.loader {
  position: absolute;
  width: 100%;
  height: 100%;
  z-index: 2;
  top: 0;
  left: 0;
  background-color: white;
}
.translucent-background {
  opacity: 0.3;
}
</style>
