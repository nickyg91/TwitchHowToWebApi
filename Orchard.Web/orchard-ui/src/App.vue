<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/DialogBox.vue';
import ToasterBlock from './components/notification/ToasterBlock.vue';
</script>
<template>
  <section class="section">
    <div class="is-flex is-align-items-center is-justify-content-center container is-vcentered">
      <RouterView />
    </div>
    <DialogBox />
    <ToasterBlock />
  </section>
</template>
<style lang="scss" scoped></style>
