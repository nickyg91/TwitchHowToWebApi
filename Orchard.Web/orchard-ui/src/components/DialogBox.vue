<template>
    <Transition name="fade">

        <div v-if="modalState.opened" v-bind:class="{ 'is-active': modalState.opened }" class="modal">
            <div class="modal-background"></div>
            <div class="modal-card">
                <header class="modal-card-head is-flex is-justify-content-space-between">
                    <div v-if="modalState.title">
                        <p class="modal-card-title">{{ modalState.title }}</p>
                    </div>
                    <div class="margin-left-auto">
                        <button @click="modal.close()" class="delete is-justify-content-end" aria-label="close"></button>
                    </div>
                </header>
                <section class="modal-card-body">
                    <component :is="modalState.view" v-model="model"></component>
                </section>
                <footer class="modal-card-foot is-justify-content-end">
                    <button class="button" v-bind:key="index" v-bind:class="action.classes.join(' ')"
                        v-for="(action, index) in modalState.actions">
                        {{ action.label }}
                    </button>
                </footer>
            </div>
        </div>
    </Transition>
</template>

<script setup lang="ts">
import { reactive } from "vue";
import { storeToRefs } from "pinia";
import useModal from "@/stores/modal.store";

const modal = useModal();

// reactive container to save the payload returned by the mounted view
const model = reactive({});

// convert all state properties to reactive references to be used on view
const { modalState } = storeToRefs(modal);
</script>

<style scoped>
.margin-left-auto {
    margin-left: auto;
}

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.5s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>