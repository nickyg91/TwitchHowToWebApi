<script setup lang="ts">
import LoadingSpinner from '@/components/loading-spinner-block/LoadingSpinner.vue';
import type { IBasketFruit } from '@/shared/interfaces/basket-fruit.interface';
import { computed } from 'vue';
const props = defineProps<{ fruit: IBasketFruit[] }>();
const isFruitPopulated = computed(() => props.fruit !== null && props.fruit.length > 0);
</script>
<template>
  <LoadingSpinner :loading="!isFruitPopulated" />
  <div class="p-3">
    <div v-for="item in fruit" :key="item.id!" class="columns">
      <div class="column">
        {{ item.fruitName }}
      </div>
      <div class="column has-text-centered">x</div>
      <div class="column has-text-right">
        {{ item.amount }}
      </div>
    </div>
  </div>
</template>
<style scoped></style>
