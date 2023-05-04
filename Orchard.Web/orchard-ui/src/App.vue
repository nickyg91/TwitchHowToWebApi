<script setup lang="ts">
import { RouterView } from 'vue-router';
import DialogBox from './components/DialogBox.vue';
import useModal from './stores/modal.store';
import BasketList from './views/baskets/components/BasketList.vue';
const modal = useModal();
function handleOpenModalClick() {
  modal.open(BasketList, 'Title', [
    {
      classes: ['is-primary'],
      label: 'Save',
      callback: () => { }
    },
    {
      classes: ['is-warning'],
      label: 'Cancel',
      callback: () => { }
    },
    {
      classes: ['is-danger'],
      label: 'Error',
      callback: () => { }
    }
  ])
}
</script>
<template>
  <section class="section">
    <div class="container">
      <button class="button is-primary" @click="handleOpenModalClick()">Open</button>
      <RouterView class="card" />
    </div>
    <DialogBox />
  </section>
</template>
<style scoped></style>
