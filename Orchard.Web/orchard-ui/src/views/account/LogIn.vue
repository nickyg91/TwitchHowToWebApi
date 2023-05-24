<script setup lang="ts">
import useModal from '@/stores/modal.store';
import { useOrchardStore } from '@/stores/orchard.store';
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';
import CreateAccountForm from './CreateAccountForm.vue';
const orchardStore = useOrchardStore();
const router = useRouter();
const formRef = ref(null);
const formValue = reactive({
  email: '',
  password: ''
});

const modal = useModal();

function createAccount() {
  modal.open(CreateAccountForm, null, null, {
    created: (isCreated: boolean) => {
      if (isCreated) {
        modal.close();
      }
    }
  });
}

async function submitLogin(): Promise<void> {
  const result = await orchardStore.logIn(formValue.email, formValue.password);
  if (result == null) {
    return;
  }
  //redirect here
  router.push({
    name: 'home'
  });
}
</script>
<template>
  <div class="login-container">
    <div class="p-2 card">
      <header class="card-header has-text-centered">
        <p class="card-header-title is-centered is-size-2">Log In</p>
      </header>
      <div class="card-content">
        <FormKit ref="formRef" #default="{ state: { valid } }" :actions="false" type="form">
          <FormKit
            type="email"
            name="email"
            label="Email"
            placeholder="Orchard@email.com"
            validation="required|email"
            v-model="formValue.email"
          />
          <FormKit
            type="password"
            name="password"
            label="Password"
            placeholder="Password"
            validation="required"
            v-model="formValue.password"
          />
          <div class="mt-5">
            <div>
              <button
                type="button"
                :disabled="!valid"
                @click="submitLogin"
                class="button ripple is-fullwidth is-success is-large"
              >
                Log In
              </button>
            </div>
            <div class="mt-2">
              <button
                @click="createAccount"
                type="button"
                class="button ripple is-fullwidth is-info is-large"
              >
                Create Account
              </button>
            </div>
          </div>
        </FormKit>
      </div>
    </div>
  </div>
</template>
<style scoped>
.login-container {
  width: 50%;
  height: 50%;
}
</style>
