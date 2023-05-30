<script setup lang="ts">
import BadgeCounter from '@/components/badge-counter/BadgeCounter.vue';
import type { IFruit } from '@/shared/models/fruit.interface';
import { useOrchardStore } from '@/stores/orchard.store';
import { computed } from 'vue';
const orchardStore = useOrchardStore();
const props = defineProps<{ fruit: IFruit }>();
const emit = defineEmits<{
  (e: 'add-to-cart', id: number, toAdd: number): void;
  (e: 'remove-from-cart', id: number, toAdd: number): void;
}>();

const currentTotalInCart = computed(() => orchardStore.cart[props.fruit.id] ?? 0);

function addToCart(id: number) {
  emit('add-to-cart', id, 1);
}

function removeFromCart(id: number) {
  emit('remove-from-cart', id, -1);
}
</script>
<template>
  <div class="card">
    <div class="card-content">
      <p class="title has-text-white">
        {{ fruit.name }}
      </p>
      <p class="subtitle">{{ fruit.skuNumber }}</p>
      <BadgeCounter :total="currentTotalInCart" />
    </div>
    <footer class="is-flex is-flex-direction-row is-justify-content-space-evenly card-footer p-2">
      <div class="card-footer-item">
        <button @click="addToCart(fruit.id)" class="button is-success is-fullwidth">Add</button>
      </div>
      <div class="card-footer-item">
        <button
          @click="removeFromCart(fruit.id)"
          :disabled="currentTotalInCart < 1"
          class="button is-danger is-fullwidth"
        >
          Remove
        </button>
      </div>
    </footer>
  </div>
</template>
<style scoped></style>
