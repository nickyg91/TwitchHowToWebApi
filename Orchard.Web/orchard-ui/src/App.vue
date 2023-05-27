<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/modal/DialogBox.vue';
import ToasterBlock from './components/notification/ToasterBlock.vue';
import TopBar from './components/TopBar.vue';
import { useOrchardStore } from './stores/orchard.store';
import { computed } from 'vue';

const orchardStore = useOrchardStore();
const isLoggedIn = computed(() => orchardStore.user !== null);
</script>
<template>
  <section>
    <div class="mt-3 is-flex is-align-items-center is-justify-content-center container">
      <TopBar v-if="isLoggedIn" />
    </div>
    <div class="section">
      <div class="container">
        <Suspense>
          <RouterView />
        </Suspense>
      </div>
    </div>
    <DialogBox />
    <ToasterBlock />
  </section>
</template>
<style lang="scss" scoped></style>
