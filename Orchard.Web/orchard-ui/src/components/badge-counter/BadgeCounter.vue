<script setup lang="ts">
defineProps<{ total: number }>();
</script>
<template>
  <div class="has-background-danger has-text-centered is-round p-1 badge">
    {{ total }}
  </div>
</template>
<style scoped>
.badge {
  width: 32px;
  height: 32px;
}
</style>
