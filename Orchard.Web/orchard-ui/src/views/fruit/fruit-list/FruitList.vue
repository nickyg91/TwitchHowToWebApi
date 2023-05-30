<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import FruitItem from '../FruitItem.vue';

const orchardStore = useOrchardStore();
const products = await orchardStore.getProducts(1, 25);

async function itemAddedOrRemoved(id: number, totalAdded: number): Promise<void> {
  await orchardStore.updateCart(id, totalAdded);
}
</script>
<template>
  <div class="box">
    <div class="columns is-multiline">
      <div v-for="fruit in products" v-bind:key="fruit.id" class="column is-one-quarter">
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
