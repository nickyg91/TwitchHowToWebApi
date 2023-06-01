<script setup lang="ts">
import { computed } from 'vue';

const props = defineProps<{
  currentPageNumber: number;
  totalPages: number;
}>();
const lastPage = computed(() => props.totalPages);

const pages = computed<number[]>(() => {
  const isLessThanFivePages = props.totalPages < 5;
  const pagesArr: number[] = [];
  if (isLessThanFivePages) {
    for (let i = 0; i < props.totalPages; i++) {
      pagesArr.push(i + 1);
    }
  } else {
    if (props.currentPageNumber >= 5) {
      for (let i = props.currentPageNumber - 1; i > props.currentPageNumber - 6; i--) {
        pagesArr.push(i + 1);
      }
    }

    for (let i = props.currentPageNumber - 1; i < props.currentPageNumber + 6; i++) {
      pagesArr.push(i + 1);
    }
  }
  return pagesArr;
});
</script>
<template>
  <div class="p-1 is-centered">
    <div class="pagination">
      <a class="pagination-previous">Previous</a>
      <a class="pagination-next">Next page</a>
      <ul class="pagination-list">
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
