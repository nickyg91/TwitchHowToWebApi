<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/DialogBox.vue';
import ToasterBlock from './components/notification/ToasterBlock.vue';
import TopBar from './components/TopBar.vue';
import { useOrchardStore } from './stores/orchard.store';

const orchardStore = useOrchardStore();
const isLoggedIn = orchardStore.orchardState.user !== null;
</script>
<template>
  <section class="section">
    <div class="is-flex is-align-items-center is-justify-content-center container is-vcentered">
      <TopBar v-if="isLoggedIn" />
      <RouterView />
    </div>
    <DialogBox />
    <ToasterBlock />
  </section>
</template>
<style lang="scss" scoped></style>
