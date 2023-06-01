<script setup lang="ts">
import { useDebounceFn } from '@vueuse/core';
import { ref } from 'vue';

defineProps<{ placeholderText?: string }>();

const searchTerm = ref('');
const debouncedSearch = useDebounceFn(() => {
  console.log('test');
}, 500);
</script>
<template>
  <div class="search-input">
    <input
      :placeholder="placeholderText ?? 'Search Items'"
      type="text"
      @input="debouncedSearch"
      v-model="searchTerm"
      class="input"
    />
  </div>
</template>

<style scoped>
.search-input {
  width: 50%;
}
</style>
