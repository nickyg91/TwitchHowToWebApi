<script setup lang="ts">
import type { CartItem } from '@/shared/cart/cart-item.model';
import { useOrchardStore } from '@/stores/orchard.store';
import { computed } from 'vue';

const orchardStore = useOrchardStore();
const cartItems = computed<CartItem[]>(() => {
  const products = orchardStore.products;
  const cartItems: CartItem[] = [];
  for (const key in orchardStore.cart) {
    const foundFruit = products?.fruit?.find((x) => x.id === Number(key));
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
const noItems = 'There are no items in your cart!';
</script>
<template>
  <div>
    <div v-if="cartItems.length > 0">
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
    </div>
    <div class="has-text-centered" v-else>
      <p class="is-size-4">
        {{ noItems }}
      </p>
    </div>
  </div>
</template>
<style scoped></style>
