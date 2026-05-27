<script setup lang="ts">
import { computed, reactive, ref } from 'vue';
import { useAuthStore } from '@/store/modules/auth';
import { useFormRules, useNaiveForm } from '@/hooks/common/form';
import { $t } from '@/locales';

defineOptions({
  name: 'PwdLogin'
});

const authStore = useAuthStore();
const { formRef, validate } = useNaiveForm();

interface FormModel {
  userName: string;
  password: string;
}

const model: FormModel = reactive({
  userName: 'admin',
  password: '1q2w3E*'
});

const rememberMe = ref(true);

const rules = computed<Record<keyof FormModel, App.Global.FormRule[]>>(() => {
  // inside computed to make locale reactive, if not apply i18n, you can define it without computed
  const { formRules } = useFormRules();

  return {
    userName: formRules.userName,
    password: formRules.pwd
  };
});

async function handleSubmit() {
  await validate();
  await authStore.login(model.userName, model.password);
}
</script>

<template>
  <NForm
    ref="formRef"
    class="operator-login-form"
    :model="model"
    :rules="rules"
    size="large"
    :show-label="false"
    @keyup.enter="handleSubmit"
  >
    <NFormItem path="userName">
      <NInput v-model:value="model.userName" :placeholder="$t('page.login.common.userNamePlaceholder')" clearable>
        <template #prefix>
          <span class="i-mdi:account-outline text-18px"></span>
        </template>
      </NInput>
    </NFormItem>
    <NFormItem path="password">
      <NInput
        v-model:value="model.password"
        type="password"
        show-password-on="click"
        :placeholder="$t('page.login.common.passwordPlaceholder')"
      >
        <template #prefix>
          <span class="i-mdi:lock-outline text-18px"></span>
        </template>
      </NInput>
    </NFormItem>
    <NSpace vertical :size="22">
      <div class="form-options">
        <NCheckbox v-model:checked="rememberMe">{{ $t('page.login.pwdLogin.rememberMe') }}</NCheckbox>
      </div>

      <NButton type="primary" size="large" block class="login-submit" :loading="authStore.loginLoading" @click="handleSubmit">
        {{ $t('route.login') }}
      </NButton>
    </NSpace>
  </NForm>
</template>

<style scoped>
.operator-login-form :deep(.n-input) {
  --n-border-radius: 8px !important;
}

.form-options {
  display: flex;
  align-items: center;
  justify-content: space-between;
  min-height: 32px;
}

.login-submit {
  height: 46px;
  border-radius: 8px;
  font-weight: 600;
}
</style>
