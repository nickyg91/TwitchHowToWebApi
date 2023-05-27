<script setup lang="ts">
import type { IFruit } from '@/shared/models/fruit.interface';

defineProps<{ fruit: IFruit }>();

// const totalInCart = computed(() => from cart store);

function addToCart() {
  // stub
}

function removeFromCart() {}
</script>
<template>
  <div class="card">
    <div class="card-content">
      <p class="title has-text-white">
        {{ fruit.name }}
      </p>
      <p class="subtitle">{{ fruit.skuNumber }}</p>
    </div>
    <footer class="is-flex is-flex-direction-row is-justify-content-space-evenly card-footer p-2">
      <div class="card-footer-item">
        <button class="button is-success is-fullwidth">Add</button>
      </div>
      <div class="card-footer-item">
        <button class="button is-danger is-fullwidth">Remove</button>
      </div>
      <!-- <p class="card-footer-item">
        <span>
          View on <a href="https://twitter.com/codinghorror/status/506010907021828096">Twitter</a>
        </span>
      </p>
      <p class="card-footer-item">
        <span> Share on <a href="#">Facebook</a> </span>
      </p> -->
    </footer>
  </div>
</template>

<style scoped></style>
