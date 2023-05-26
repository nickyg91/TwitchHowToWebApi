<script setup lang="ts">
import { RouterLink } from 'vue-router';
import type { MenuItem } from './models/menu-item.model';
const props = defineProps<{ menuItems: MenuItem[] }>();
</script>
<template>
  <div class="menu has-background-black-ter">
    <ul class="menu-list">
      <li v-for="(item, index) in props.menuItems" v-bind:key="index">
        <RouterLink :to="item.route" :key="index" :active-class="'is-active'">
          <span v-if="item.icon" class="icon">
            <i :class="item.icon"></i>
          </span>
          {{ item.text }}
        </RouterLink>
      </li>
    </ul>
  </div>
</template>
<style scoped></style>
