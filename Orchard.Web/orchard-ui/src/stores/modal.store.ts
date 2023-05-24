import { defineStore } from 'pinia';
import { reactive, type Component, markRaw } from 'vue';
type EventEmitterType = { [key in string]: Function };
export type Modal = {
  title?: string | null;
  opened: boolean;
  view: Component;
  actions?: ModalAction[] | null;
  events: EventEmitterType | undefined;
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
    actions: [],
    events: undefined
  });

  function open(
    view: Component,
    title?: string | null,
    actions?: ModalAction[] | null,
    events?: EventEmitterType
  ) {
    modalState.title = title;
    modalState.opened = true;
    modalState.view = markRaw(view);
    modalState.actions = actions;
    modalState.events = events;
  }

  function close(): void {
    modalState.title = null;
    modalState.opened = false;
    modalState.view = {};
    modalState.actions = [];
    modalState.events = {};
  }

  return {
    modalState,
    open,
    close
  };
});

export default useModal;
