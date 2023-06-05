<script setup lang="ts">
import { computed, ref } from 'vue';

const props = defineProps<{
  totalPages: number;
}>();
const currentPage = ref(1);

const lastPage = computed(() => props.totalPages);

const emits = defineEmits<{
  nextClicked: [currentPage: number];
  previousClicked: [currentPage: number];
  pageClicked: [pageNumber: number];
}>();

const previousDisabled = computed(() => {
  return currentPage.value === 1;
});

const nextDisabled = computed(() => {
  return currentPage.value === lastPage.value;
});

const pages = computed<number[]>(() => {
  const isLessThanFivePages = lastPage.value <= 5;
  const hasFiveOrLessPagesBeforeLastPage = lastPage.value - currentPage.value <= 5;

  const pagesArr: number[] = [];

  if (hasFiveOrLessPagesBeforeLastPage) {
    for (let i = currentPage.value; i < lastPage.value; i++) {
      pagesArr.push(i);
    }
    return pagesArr;
  }

  if (isLessThanFivePages) {
    for (let i = 1; i < lastPage.value; i++) {
      pagesArr.push(i);
    }
    return pagesArr;
  }

  if (!isLessThanFivePages) {
    for (let i = currentPage.value; i < currentPage.value + 5; i++) {
      pagesArr.push(i);
    }
    return pagesArr;
  }
  return pagesArr;
});

function nextClicked(): void {
  if (currentPage.value === lastPage.value) {
    return;
  }
  currentPage.value += 1;
  emits('nextClicked', currentPage.value);
}

function previousClicked(): void {
  if (currentPage.value === 1) {
    return;
  }
  currentPage.value -= 1;
  emits('previousClicked', currentPage.value);
}

function pageClicked(pageNumber: number): void {
  currentPage.value = pageNumber;
  emits('pageClicked', currentPage.value);
}
</script>
<template>
  <nav class="p-1">
    <div class="pagination is-centered">
      <button
        class="pagination-previous button"
        @click="previousClicked"
        :disabled="previousDisabled"
      >
        Previous
      </button>
      <button class="pagination-next button" @click="nextClicked" :disabled="nextDisabled">
        Next page
      </button>
      <ul class="pagination-list">
        <li v-for="page in pages" :key="page">
          <a
            @click="pageClicked(page)"
            :class="{ 'is-current': currentPage === page }"
            class="pagination-link"
            >{{ page }}</a
          >
        </li>
        <li>
          <span class="pagination-ellipsis">&hellip;</span>
        </li>
        <li>
          <a :class="{ 'is-current': currentPage === lastPage }" class="pagination-link">{{
            lastPage
          }}</a>
        </li>
      </ul>
    </div>
  </nav>
</template>
<style scoped></style>
