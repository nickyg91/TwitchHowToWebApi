<script setup lang="ts">
import type { CartItem } from '@/shared/cart/cart-item.model';
import { useOrchardStore } from '@/stores/orchard.store';
import { computed } from 'vue';

const orchardStore = useOrchardStore();
const cartItems = computed<CartItem[]>(() => {
  const fruit = orchardStore.products;
  const cartItems: CartItem[] = [];
  for (const key in orchardStore.cart) {
    const foundFruit = fruit?.find((x) => x.id === Number(key));
    if (
      Object.prototype.hasOwnProperty.call(orchardStore.cart, key) &&
      orchardStore.cart[key] > 0 &&
      foundFruit
    ) {
      cartItems.push({
        fruit: foundFruit,
        text: foundFruit.name,
        total: orchardStore.cart[key]
      } as CartItem);
    }
  }
  return cartItems;
});

async function removeFromCart(id: number): Promise<void> {
  const totalToRemove = orchardStore.cart[id] * -1;
  await orchardStore.updateCart(id, totalToRemove);
}
</script>
<template>
  <div
    v-for="item in cartItems"
    :key="item.fruit.id"
    class="p-2 is-flex is-flex-direction-row is-justify-content-space-between"
  >
    <div>
      {{ item.text }}
    </div>
    <div>
      {{ item.total }}
    </div>
    <div>
      <button @click="removeFromCart(item.fruit.id)" class="button is-danger is-round">
        <span class="icon">
          <i class="fa fa-remove"></i>
        </span>
      </button>
    </div>
  </div>
</template>
<style scoped></style>
