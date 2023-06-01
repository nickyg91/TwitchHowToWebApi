<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  currentPageNumber: number;
  totalPages: number;
}>();
const firstPage = 1;
const lastPage = computed(() => props.totalPages);

const pages = computed<number[]>(() => {
  const middle = Math.floor(props.totalPages);
  return [middle - 1, middle, middle + 1];
});
</script>
<template>
  <div class="p-1 is-flex is-justify-content-center">
    <div class="pagination">
      <ul class="pagination-list">
        <li>
          <a :class="{ 'is-current': currentPageNumber === firstPage }" class="pagination-link">{{
            firstPage
          }}</a>
        </li>
        <li>
          <span class="pagination-ellipsis">&hellip;</span>
        </li>
        <li v-for="page in pages" :key="page">
          <a :class="{ 'is-current': currentPageNumber === page }" class="pagination-link">{{
            page
          }}</a>
        </li>
        <li>
          <span class="pagination-ellipsis">&hellip;</span>
        </li>
        <li>
          <a :class="{ 'is-current': currentPageNumber === lastPage }" class="pagination-link">{{
            lastPage
          }}</a>
        </li>
      </ul>
    </div>
  </div>
</template>

<style scoped></style>
