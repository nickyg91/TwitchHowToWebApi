<script setup lang="ts">
import { ref } from 'vue';
import { CreateAccountModel } from './models/create-account.model';
import { FormKit } from '@formkit/vue';
import type { FormKitContext } from '@formkit/core';
import { useOrchardStore } from '@/stores/orchard.store';
const orchardState = useOrchardStore();
const formRef = ref<any | null>(null);
const emit = defineEmits<{ (e: 'created', value: boolean): void }>();

function validPassword(node: FormKitContext) {
  const value = node.value as string;
  const digits = /[0-9]/;
  const lowerCaseLetters = /[a-z]/;
  const upperCaseLetters = /[A-Z]/;
  const symbols = /[!"#$%&'()*+,-./:;<=>?@[\\\]^_`{|}~]/g;

  return (
    digits.test(value) &&
    lowerCaseLetters.test(value) &&
    upperCaseLetters.test(value) &&
    symbols.test(value) &&
    value.length >= 8 &&
    value.length <= 32
  );
}
const formValue = ref(new CreateAccountModel('', '', ''));
const passwordMessages = {
  validPassword:
    'Password must be between 8 and 32 characters, contain a number, symbol, and one capital letter.'
};

async function createAccount() {
  const isValid = formRef.value?.node?.context?.state.valid;
  if (!isValid) {
    return;
  }
  const res = await orchardState.createAccount(formValue.value);
  emit('created', res);
}
</script>

<template>
  <section class="section">
    <div class="card">
      <FormKit ref="formRef" #default="{ state: { valid } }" :actions="false" type="form">
        <div class="card-content">
          <FormKit
            type="text"
            name="firstName"
            label="First Name"
            help="Your first name."
            placeholder="Orchard User First Name"
            validation="required"
            v-model="formValue.firstName"
          />
          <FormKit
            type="text"
            name="lastName"
            label="Last Name"
            help="Your Last name."
            placeholder="Orchard User Last Name"
            validation="required"
            v-model="formValue.lastName"
          />
          <FormKit
            type="email"
            name="email"
            label="Email"
            help="your@email.com"
            placeholder="Orchard@email.com"
            validation="required|email"
            v-model="formValue.email"
          />
          <FormKit
            type="password"
            name="password"
            label="Password"
            placeholder="Password"
            :validation-rules="{ validPassword }"
            validation="required|validPassword"
            :validation-messages="passwordMessages"
            validation-visibility="live"
            v-model="formValue.password"
          />
          <FormKit
            type="password"
            name="password_confirm"
            label="Confirm Password"
            placeholder="Confirm Password"
            validation="required|confirm"
            validation-visibility="live"
            v-model="formValue.confirmPassword"
          />
          <div class="mt-3 is-flex is-flex-direction-row is-justify-content-end is-flex-grow-1">
            <button
              type="button"
              :disabled="!valid"
              @click="createAccount"
              class="button is-primary mr-2"
            >
              <span class="icon">
                <i class="fa-solid fa-check"></i>
              </span>
              <span>Submit</span>
            </button>
          </div>
        </div>
      </FormKit>
    </div>
  </section>
</template>
<style scoped></style>
