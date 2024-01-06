<template>
  <div style="height: 250px">
    <lay-form :model="loginForm" ref="formRef" :rules="rules">
      <lay-form-item :label-width="0" prop="emailAddress">
        <lay-input prefix-icon="layui-icon-email" placeholder="邮箱" v-model="loginForm.emailAddress"></lay-input>
      </lay-form-item>
      <lay-form-item :label-width="0" prop="verificationCode">
        <div style="width: 264px; display: inline-block">
          <lay-input :allow-clear="true" prefix-icon="layui-icon-vercode" placeholder="验证码"
            v-model="loginForm.verificationCode"></lay-input>
        </div>

        <div class="login-captach" @click="toRefreshImg">
          <img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
        </div>
      </lay-form-item>
      <lay-form-item :label-width="0" prop="secondCode">
        <lay-input prefix-icon="layui-icon-vercode" placeholder="请输入邮箱验证码" v-model="loginForm.secondCode">
          <template #append>
            <span :class="isDisable ? 'disabled': 'enabled'" @click="sendEmail">{{ isDisable ? btnText : '发送验证码'}}</span></template>
        </lay-input>
      </lay-form-item>
      <div style="height: 19.2px"></div>
      <lay-form-item :label-width="0">
        <lay-button style="margin-top: 20px" type="primary" :loading="loging" :fluid="true"
          loadingIcon="layui-icon-loading" @click="loginSubmit">登录/注册</lay-button>
      </lay-form-item>
    </lay-form>
  </div>
</template>
<script setup lang="ts">
import { login } from "../../../api/module/user"
import { verificationImg } from "../../../api/module/common"
import { sendEmailVerificationCode } from "../../../api/module/common"
import { onMounted, reactive, ref } from "vue"
import { useRouter } from "vue-router"
import { useUserStore } from "./../../../store/user"
import { layer } from "@layui/layer-vue"

const formRef = ref()
const router = useRouter();
const userStore = useUserStore();
const verificationImgUrl = ref("");
const loging = ref(false);
const btnText = ref('60秒后再试')
const isDisable = ref(false)
const loginForm = reactive({
  emailAddress: '',
  verificationCode: '',
  captchaId: '',
  secondCode: '',
  loginWay: 2
});
const rules = ref({
  emailAddress:[{
    required: true,
    message: '邮箱不能为空'
  },{
    type: 'email',
    message: '邮箱不合法'
  }],
  verificationCode:{
    required: true,
    message: '验证码不能为空'
  },
  secondCode:{
    required: true,
    message: '邮箱验证码不能为空'
  }
})

onMounted(() => {
  toRefreshImg();
});

const toRefreshImg = async () => {
  let res = await verificationImg();
  if (!res.hasError) {
    verificationImgUrl.value = "data:image/gif;base64," + res.img;
    loginForm.captchaId = res.captchaId;
  }
};

const loginSubmit = () => {
  formRef.value.validate(async (isValidate: boolean)=>{
    if (!isValidate)
      return false;
    loging.value = true;
    let res = await login(loginForm);
    if (!res.hasError) {
      setTimeout(() => {
        loging.value = false;
        layer.msg("登录成功", { icon: 1 }, async () => {
          userStore.token = res.token;
          await userStore.loadMenus();
          await userStore.loadPermissions();
          router.push("/");
        });
      }, 1000);
    } else {
      loging.value = false;
      toRefreshImg();
    }
  })
};

const sendEmail = async () => {
  formRef.value.validate(['emailAddress', 'verificationCode'], async (isValidate : boolean)=>{
    if (!isValidate)
      return false;
    let sendForm = {
      captchaId: loginForm.captchaId,
      verificationCode: loginForm.verificationCode,
      emailAddress: loginForm.emailAddress
    }
    let res = await sendEmailVerificationCode(sendForm);
    if (!res.hasError){
      layer.msg('发送成功');
      isDisable.value = true;
      startCountdown();
    }else{
      toRefreshImg();
    }
  })
};

const startCountdown = ()=>{
  let time = 60;
  let intervalId = setInterval(()=>{
    if (time > 0){
      btnText.value = time + '秒后再试';
      time -- ;
    }else{
      clearInterval(intervalId);
      isDisable.value = false;
      btnText.value = '60秒后再试';
    }
  },1000)
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

.enabled{
  cursor: pointer; 
  color: #16baaa
}

.disabled{
  pointer-events: none;
}
</style>
