<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import FruitItem from '../FruitItem.vue';
import { ref } from 'vue';
import PaginationBar from '@/components/pagination/PaginationBar.vue';

const orchardStore = useOrchardStore();
const perPage = ref(25);
let products = ref(await orchardStore.getProducts(1, perPage.value));

async function itemAddedOrRemoved(id: number, totalAdded: number): Promise<void> {
  await orchardStore.updateCart(id, totalAdded);
}

async function pageChanged(pageNumber: number): Promise<void> {
  products.value = await orchardStore.getProducts(pageNumber, perPage.value);
}
</script>
<template>
  <div class="box">
    <PaginationBar
      @next-clicked="pageChanged($event)"
      @previous-clicked="pageChanged($event)"
      @page-clicked="pageChanged($event)"
      :total-pages="products.totalPages"
    />
    <div class="mt-2 columns is-multiline">
      <div v-for="fruit in products.fruit" v-bind:key="fruit.id" class="column is-one-quarter">
        <FruitItem
          :fruit="fruit"
          :key="fruit.id"
          @add-to-cart="itemAddedOrRemoved"
          @remove-from-cart="itemAddedOrRemoved"
        />
      </div>
    </div>
  </div>
</template>

<style scoped></style>
