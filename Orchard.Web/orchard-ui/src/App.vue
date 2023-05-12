<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/DialogBox.vue';

</script>
<template>
  <section class="section">
    <div class="container">
      <RouterView class="card" />
    </div>
    <DialogBox />
  </section>
</template>
<style scoped></style>
