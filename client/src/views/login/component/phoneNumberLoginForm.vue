<template>
  <div style="height: 250px">
    <lay-form-item :label-width="0">
      <lay-input prefix-icon="layui-icon-cellphone" placeholder="手机号" v-model="loginForm.phoneNumber"></lay-input>
    </lay-form-item>
    <lay-form-item :label-width="0">
      <div style="width: 264px; display: inline-block">
        <lay-input :allow-clear="true" prefix-icon="layui-icon-vercode" placeholder="验证码"
          v-model="loginForm.verificationCode"></lay-input>
      </div>

      <div class="login-captach" @click="toRefreshImg">
        <img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
      </div>
    </lay-form-item>
    <lay-form-item :label-width="0">
      <lay-input prefix-icon="layui-icon-vercode" placeholder="请输入短信验证码" v-model="loginForm.secondCode">
        <template #append><span style="cursor: pointer; color: #16baaa" @click="sendSms">发送验证码</span></template>
      </lay-input>
    </lay-form-item>
    <div style="height: 19.2px"></div>
    <lay-form-item :label-width="0">
      <lay-button style="margin-top: 20px" type="primary" :loading="loging" :fluid="true" loadingIcon="layui-icon-loading"
        @click="loginSubmit">登录/注册</lay-button>
    </lay-form-item>
  </div>
</template>
<script setup lang="ts">
import { login } from '../../../api/module/user'
import { verificationImg } from '../../../api/module/common'
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from './../../../store/user'
import { layer } from '@layui/layer-vue'

const router = useRouter()
const userStore = useUserStore()
const verificationImgUrl = ref('')
const loging = ref(false);
const loginForm = reactive({
  phoneNumber: '',
  verificationCode: '',
  captchaId: '',
  secondCode: '',
  loginWay: 1
})

onMounted(() => {
  toRefreshImg()
})

const toRefreshImg = async () => {
  let res = await verificationImg();
  if (!res.hasError) {
    verificationImgUrl.value = 'data:image/gif;base64,' + res.img;
    loginForm.captchaId = res.captchaId;
  }
}

const loginSubmit = async () => {
  layer.msg('暂不支持')
  return;
  // loging.value = true;
  // let res = await login(loginForm);
  // if (!res.hasError) {
  //   setTimeout(() => {
  //     loging.value = false;
  //     layer.msg('登录成功', { icon: 1 }, async () => {
  //       userStore.token = res.token
  //       await userStore.loadMenus()
  //       await userStore.loadPermissions()
  //       router.push('/')
  //     })
  //   }, 1000);
  // }
  // else {
  //   loging.value = false;
  //   toRefreshImg();
  // }
}

const sendSms = async () => {
  layer.msg('暂不支持')
}

</script>
<style scoped>
.login-captach {
  display: inline-block;
  vertical-align: bottom;
  width: 108px;
  height: 40px;
  color: var(--global-primary-color);
  margin-left: 8px;
  border-radius: 4px;
  border: 1px solid hsla(0, 0%, 60%, 0.46);
  transition: border 0.2s;
  box-sizing: border-box;
  background: #fff;
  overflow: hidden;
  cursor: pointer;
}
</style>
