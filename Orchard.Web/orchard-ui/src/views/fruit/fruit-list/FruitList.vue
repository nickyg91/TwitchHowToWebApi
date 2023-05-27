<script setup lang="ts">
import { useOrchardStore } from '@/stores/orchard.store';
import FruitItem from '../FruitItem.vue';

const orchardStore = useOrchardStore();
const products = await orchardStore.getProducts(1, 25);
</script>
<template>
  <div class="box">
    <div class="columns is-multiline">
      <div v-for="fruit in products" v-bind:key="fruit.id" class="column is-4">
        <FruitItem :fruit="fruit" />
      </div>
    </div>
  </div>
</template>

<style scoped></style>
