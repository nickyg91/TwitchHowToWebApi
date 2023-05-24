<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/DialogBox.vue';
import ToasterBlock from './components/notification/ToasterBlock.vue';
</script>
<template>
  <section class="section">
    <div class="container">
      <RouterView class="card" />
    </div>
    <DialogBox />
    <ToasterBlock />
  </section>
</template>
<style lang="scss" scoped></style>
