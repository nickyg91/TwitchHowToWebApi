<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import FruitItem from '../FruitItem.vue';
import { ref } from 'vue';
import PaginationBarVue from '@/components/pagination/PaginationBar.vue';

const orchardStore = useOrchardStore();
const page = ref(1);
const perPage = ref(25);
const products = await orchardStore.getProducts(page.value, perPage.value);

async function itemAddedOrRemoved(id: number, totalAdded: number): Promise<void> {
  await orchardStore.updateCart(id, totalAdded);
}
</script>
<template>
  <div class="box">
    <PaginationBarVue :total-pages="products.totalPages" :current-page-number="page" />
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
