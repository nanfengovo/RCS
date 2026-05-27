<script setup lang="ts">
import { computed } from 'vue';
import type { Component } from 'vue';
import { loginModuleRecord } from '@/constants/app';
import { useAppStore } from '@/store/modules/app';
import { useThemeStore } from '@/store/modules/theme';
import { $t } from '@/locales';
import PwdLogin from './modules/pwd-login.vue';
import CodeLogin from './modules/code-login.vue';
import Register from './modules/register.vue';
import ResetPwd from './modules/reset-pwd.vue';
import BindWechat from './modules/bind-wechat.vue';

interface Props {
  /** The login module */
  module?: UnionKey.LoginModule;
}

const props = defineProps<Props>();

const appStore = useAppStore();
const themeStore = useThemeStore();

interface LoginModule {
  label: App.I18n.I18nKey;
  component: Component;
}

const moduleMap: Record<UnionKey.LoginModule, LoginModule> = {
  'pwd-login': { label: loginModuleRecord['pwd-login'], component: PwdLogin },
  'code-login': { label: loginModuleRecord['code-login'], component: CodeLogin },
  register: { label: loginModuleRecord.register, component: Register },
  'reset-pwd': { label: loginModuleRecord['reset-pwd'], component: ResetPwd },
  'bind-wechat': { label: loginModuleRecord['bind-wechat'], component: BindWechat }
};

const activeModule = computed(() => moduleMap[props.module || 'pwd-login']);
</script>

<template>
  <div class="login-shell" :class="{ 'is-dark': themeStore.darkMode }">
    <div class="login-grid-bg"></div>

    <div class="login-toolbar">
      <ThemeSchemaSwitch
        :theme-schema="themeStore.themeScheme"
        :show-tooltip="false"
        class="text-20px"
        @switch="themeStore.toggleThemeScheme"
      />
      <LangSwitch
        v-if="themeStore.header.multilingual.visible"
        :lang="appStore.locale"
        :lang-options="appStore.localeOptions"
        :show-tooltip="false"
        @change-lang="appStore.changeLocale"
      />
    </div>

    <section class="login-stage">
      <div class="login-identity">
        <div class="brand-row">
          <SystemLogo class="brand-logo" />
          <div>
            <p class="brand-kicker">RCS CONTROL SUITE</p>
            <h1>{{ $t('system.title') }}</h1>
          </div>
        </div>

        <div class="dispatch-map" aria-hidden="true">
          <div class="map-track track-a"></div>
          <div class="map-track track-b"></div>
          <div class="map-track track-c"></div>
          <div class="node node-a"><span></span></div>
          <div class="node node-b"><span></span></div>
          <div class="node node-c"><span></span></div>
          <div class="carrier carrier-a"></div>
          <div class="carrier carrier-b"></div>
        </div>

        <div class="status-strip">
          <div>
            <span>AMHS</span>
            <strong>READY</strong>
          </div>
          <div>
            <span>WMS</span>
            <strong>ONLINE</strong>
          </div>
          <div>
            <span>DEVICE</span>
            <strong>SYNC</strong>
          </div>
        </div>
      </div>

      <NCard :bordered="false" class="login-card">
        <div class="login-card-inner">
          <header class="login-card-header">
            <p class="login-card-kicker">OPERATOR ACCESS</p>
            <h2>{{ $t(activeModule.label) }}</h2>
          </header>

          <Transition :name="themeStore.page.animateMode" mode="out-in" appear>
            <component :is="activeModule.component" />
          </Transition>
        </div>
      </NCard>
    </section>
  </div>
</template>

<style scoped>
.login-shell {
  --login-page-bg:
    radial-gradient(circle at 18% 18%, rgb(8 145 178 / 18%), transparent 30%),
    linear-gradient(135deg, #edf6f8 0%, #dbe8ee 42%, #f8fafc 42%, #eef2f7 100%);
  --login-grid-line: rgb(15 23 42 / 8%);
  --login-grid-mask: linear-gradient(90deg, rgb(0 0 0 / 42%), transparent 72%);
  --login-toolbar-color: #102033;
  --login-panel-text: #0f172a;
  --login-muted: #475569;
  --login-accent: #0891b2;
  --login-accent-strong: #0e7490;
  --login-warn: #d97706;
  --login-brand-logo-bg: rgb(255 255 255 / 64%);
  --login-brand-logo-border: rgb(14 116 144 / 24%);
  --login-map-bg: rgb(255 255 255 / 50%);
  --login-map-line: rgb(14 116 144 / 16%);
  --login-map-border: rgb(14 116 144 / 24%);
  --login-map-shadow: inset 0 0 0 1px rgb(255 255 255 / 74%);
  --login-node-bg: rgb(236 254 255 / 92%);
  --login-node-border: rgb(8 145 178 / 72%);
  --login-status-bg: rgb(255 255 255 / 68%);
  --login-status-border: rgb(14 116 144 / 22%);
  --login-card-bg: rgb(255 255 255 / 92%);
  --login-card-shadow: 0 24px 70px rgb(15 23 42 / 18%);
  --login-card-title: #0f172a;

  position: relative;
  min-height: 100%;
  overflow: hidden;
  color: var(--login-panel-text);
  background: var(--login-page-bg);
}

.login-shell.is-dark {
  --login-page-bg:
    radial-gradient(circle at 18% 18%, rgb(34 211 238 / 20%), transparent 31%),
    linear-gradient(135deg, #06111f 0%, #0f172a 48%, #111827 48%, #020617 100%);
  --login-grid-line: rgb(255 255 255 / 7%);
  --login-grid-mask: linear-gradient(90deg, rgb(0 0 0 / 82%), transparent 74%);
  --login-toolbar-color: #e2e8f0;
  --login-panel-text: #e5edf7;
  --login-muted: #94a3b8;
  --login-accent: #67e8f9;
  --login-accent-strong: #22d3ee;
  --login-warn: #f59e0b;
  --login-brand-logo-bg: rgb(255 255 255 / 10%);
  --login-brand-logo-border: rgb(255 255 255 / 16%);
  --login-map-bg: rgb(15 23 42 / 50%);
  --login-map-line: rgb(148 163 184 / 12%);
  --login-map-border: rgb(148 163 184 / 28%);
  --login-map-shadow: inset 0 0 0 1px rgb(255 255 255 / 4%);
  --login-node-bg: rgb(8 47 73 / 78%);
  --login-node-border: rgb(103 232 249 / 70%);
  --login-status-bg: rgb(15 23 42 / 54%);
  --login-status-border: rgb(148 163 184 / 24%);
  --login-card-bg: rgb(15 23 42 / 84%);
  --login-card-shadow: 0 24px 70px rgb(0 0 0 / 32%);
  --login-card-title: #f8fafc;
}

.login-grid-bg {
  position: absolute;
  inset: 0;
  background-image:
    linear-gradient(var(--login-grid-line) 1px, transparent 1px),
    linear-gradient(90deg, var(--login-grid-line) 1px, transparent 1px);
  background-size: 36px 36px;
  mask-image: var(--login-grid-mask);
}

.login-toolbar {
  position: absolute;
  top: 28px;
  right: 32px;
  z-index: 3;
  display: flex;
  gap: 12px;
  align-items: center;
  color: var(--login-toolbar-color);
}

.login-stage {
  position: relative;
  z-index: 2;
  display: grid;
  grid-template-columns: minmax(360px, 1fr) 430px;
  gap: 56px;
  align-items: center;
  width: min(1180px, calc(100% - 48px));
  min-height: 100vh;
  margin: 0 auto;
  padding: 56px 0;
}

.login-identity {
  color: var(--login-panel-text);
}

.brand-row {
  display: flex;
  gap: 20px;
  align-items: center;
}

.brand-logo {
  width: 72px;
  height: 72px;
  padding: 10px;
  color: var(--login-accent);
  background: var(--login-brand-logo-bg);
  border: 1px solid var(--login-brand-logo-border);
  border-radius: 8px;
}

.brand-kicker,
.login-card-kicker {
  margin: 0 0 8px;
  font-size: 12px;
  font-weight: 700;
  line-height: 1;
  letter-spacing: 0;
}

.brand-kicker {
  color: var(--login-accent-strong);
}

.brand-row h1 {
  max-width: 560px;
  margin: 0;
  font-size: 42px;
  font-weight: 700;
  line-height: 1.15;
  letter-spacing: 0;
}

.dispatch-map {
  position: relative;
  height: 300px;
  margin: 64px 0 36px;
  border: 1px solid var(--login-map-border);
  border-radius: 8px;
  background:
    linear-gradient(90deg, var(--login-map-line) 1px, transparent 1px),
    linear-gradient(var(--login-map-line) 1px, transparent 1px),
    var(--login-map-bg);
  background-size: 44px 44px;
  box-shadow: var(--login-map-shadow);
}

.map-track {
  position: absolute;
  height: 2px;
  background: linear-gradient(90deg, transparent, var(--login-accent), transparent);
}

.track-a {
  top: 84px;
  right: 13%;
  left: 10%;
}

.track-b {
  top: 168px;
  right: 8%;
  left: 22%;
}

.track-c {
  top: 230px;
  right: 22%;
  left: 8%;
}

.node {
  position: absolute;
  width: 34px;
  height: 34px;
  border: 1px solid var(--login-node-border);
  border-radius: 8px;
  background: var(--login-node-bg);
}

.node span {
  position: absolute;
  inset: 9px;
  border-radius: 4px;
  background: var(--login-warn);
}

.node-a {
  top: 67px;
  left: 18%;
}

.node-b {
  top: 151px;
  right: 18%;
}

.node-c {
  top: 213px;
  left: 44%;
}

.carrier {
  position: absolute;
  width: 72px;
  height: 26px;
  border: 1px solid rgb(245 158 11 / 80%);
  border-radius: 6px;
  background: linear-gradient(90deg, var(--login-warn), #fde68a);
  box-shadow: 0 0 18px rgb(245 158 11 / 26%);
}

.carrier-a {
  top: 71px;
  right: 28%;
}

.carrier-b {
  top: 217px;
  left: 20%;
}

.status-strip {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 12px;
}

.status-strip div {
  padding: 14px 16px;
  border: 1px solid var(--login-status-border);
  border-radius: 8px;
  background: var(--login-status-bg);
}

.status-strip span {
  display: block;
  margin-bottom: 4px;
  color: var(--login-muted);
  font-size: 12px;
}

.status-strip strong {
  color: var(--login-accent-strong);
  font-size: 16px;
  letter-spacing: 0;
}

.login-card {
  --n-color: var(--login-card-bg) !important;
  --n-color-modal: var(--login-card-bg) !important;
  --n-color-popover: var(--login-card-bg) !important;

  width: 100%;
  border-radius: 8px;
  background: var(--login-card-bg);
  box-shadow: var(--login-card-shadow);
}

.login-card :deep(.n-card__content) {
  background-color: var(--login-card-bg);
}

.login-card-inner {
  padding: 8px 4px;
}

.login-card-header {
  margin-bottom: 26px;
}

.login-card-kicker {
  color: var(--login-accent-strong);
}

.login-card-header h2 {
  margin: 0;
  color: var(--login-card-title);
  font-size: 28px;
  font-weight: 700;
  line-height: 1.2;
  letter-spacing: 0;
}

@media (max-width: 900px) {
  .login-shell {
    --login-page-bg: linear-gradient(180deg, #edf6f8 0%, #dbe8ee 44%, #f8fafc 44%, #eef2f7 100%);
  }

  .login-shell.is-dark {
    --login-page-bg: linear-gradient(180deg, #06111f 0%, #0f172a 44%, #111827 44%, #020617 100%);
  }

  .login-stage {
    grid-template-columns: 1fr;
    gap: 28px;
    width: min(460px, calc(100% - 32px));
    padding: 88px 0 32px;
  }

  .brand-row h1 {
    font-size: 28px;
  }

  .brand-logo {
    width: 56px;
    height: 56px;
  }

  .dispatch-map {
    display: none;
  }

  .status-strip {
    margin-top: 28px;
  }
}

@media (max-width: 520px) {
  .login-toolbar {
    top: 18px;
    right: 18px;
  }

  .status-strip {
    grid-template-columns: 1fr;
  }

  .login-card-header h2 {
    font-size: 24px;
  }
}
</style>
