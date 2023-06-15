<script setup lang="ts" generic="T extends MenuItem">
import { RouterLink } from 'vue-router';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { MenuItem } from './models/menu-item.model';
import { ref } from 'vue';
defineProps<{ menuItems: T[]; iconClass: string }>();

const showMenu = ref(false);
function onBlur(event: FocusEvent) {
  const element = event.relatedTarget as HTMLElement;
  if (!element || !element.attributes.getNamedItem('href')) {
    showMenu.value = false;
  }
}

function onClick() {
  showMenu.value = !showMenu.value;
}
</script>
<template>
  <div class="dropdown" :class="{ 'is-active': showMenu }">
    <div class="dropdown-trigger">
      <button @blur="onBlur" @click="onClick" class="button is-round is-warning is-ghost">
        <span class="icon">
          <i :class="iconClass"></i>
        </span>
      </button>
    </div>
    <div class="dropdown-menu">
      <div class="dropdown-content">
        <div class="menu has-background-black">
          <ul class="menu-list">
            <li
              @click="$event.stopImmediatePropagation()"
              v-for="(item, index) in menuItems"
              v-bind:key="index"
            >
              <slot>
                <RouterLink
                  v-if="item.route"
                  :to="item.route"
                  :key="index"
                  :active-class="'is-active'"
                >
                  <span v-if="item.icon" class="icon">
                    <i :class="item.icon"></i>
                  </span>
                  {{ item.text }}
                </RouterLink>
              </slot>
              <slot name="content" v-bind="item"></slot>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped></style>
