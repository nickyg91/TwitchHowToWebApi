import { defineStore } from 'pinia';
import { reactive, type Component, markRaw } from 'vue';

export type Modal = {
  title?: string | null;
  opened: boolean;
  view: Component;
  actions?: ModalAction[];
};
export type ModalAction = {
  label: string;
  classes: string[];
  callback: (props?: any) => void | Promise<void>;
};
export const useModal = defineStore('modalStore', () => {
  const modalState: Modal = reactive({
    title: null,
    opened: false,
    view: {},
    actions: []
  });

  function open(view: Component, title?: string, actions?: ModalAction[]) {
    modalState.title = title;
    modalState.opened = true;
    modalState.view = markRaw(view);
    modalState.actions = actions;
  }

  function close(): void {
    modalState.title = null;
    modalState.opened = false;
    modalState.view = {};
    modalState.actions = [];
  }

  return {
    modalState,
    open,
    close
  };
});

export default useModal;
