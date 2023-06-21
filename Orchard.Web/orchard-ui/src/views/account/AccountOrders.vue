<script setup lang="ts">
import { OrderStatus } from '@/shared/enums/order-status.enum';
import { useOrderStore } from '@/stores/orders.store';
import { DateTime } from 'luxon';
import { ref } from 'vue';

const ordersStore = useOrderStore();
await ordersStore.getOrders();

const expandedItems = ref<Set<number>>(new Set<number>());

function readableOrderStatus(orderStatus: OrderStatus): string {
  return OrderStatus[orderStatus].toString();
}

function formattedDate(date: string): string {
  return DateTime.fromISO(date).toLocaleString();
}

function isExpanded(id: number): boolean {
  return expandedItems.value.has(id);
}

function expand(id: number): void {
  if (expandedItems.value.has(id)) {
    expandedItems.value.delete(id);
    return;
  }
  expandedItems.value.add(id);
}
</script>
<template>
  <div class="content">
    <table class="table is-striped is-hoverable">
      <thead>
        <tr>
          <th>Order Status</th>
          <th>Date Ordered</th>
          <th>Total Items</th>
        </tr>
      </thead>
      <tbody>
        <tr @click="expand(order.id!)" v-for="order in ordersStore.state.orders" :key="order.id!">
          <td class="has-text-left">
            {{ readableOrderStatus(order.orderStatus!) }}
          </td>
          <td class="has-text-left">
            {{ formattedDate(order.createdAtUtc!) }}
          </td>
          <td class="has-text-right">
            {{ order.totalFruit }}
          </td>
        </tr>
        <tr v-for="order in ordersStore.state.orders" :key="order.id!">
          <td v-if="isExpanded(order.id!)" colspan="3">expanded</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped></style>
