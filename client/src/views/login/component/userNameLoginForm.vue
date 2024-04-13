<template>
  <div style="height: 250px">
    <lay-form :model="loginForm" ref="formRef" :rules="rules">
			<lay-form-item :label-width="0" prop="userName">
				<lay-input
						:allow-clear="true"
						prefix-icon="layui-icon-username"
						placeholder="用户名"
						v-model="loginForm.userName"
				></lay-input>
			</lay-form-item>
			<lay-form-item :label-width="0" prop="password">
				<lay-input
						:allow-clear="true"
						prefix-icon="layui-icon-password"
						placeholder="密码"
						password
						type="password"
						v-model="loginForm.password"
				></lay-input>
			</lay-form-item>
			<lay-form-item :label-width="0" prop="verificationCode">
				<div style="width: 264px; display: inline-block">
						<lay-input
						:allow-clear="true"
						prefix-icon="layui-icon-vercode"
						placeholder="验证码"
						v-model="loginForm.verificationCode"
						></lay-input>
				</div>
				<div class="login-captach" @click="toRefreshImg">
						<img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
				</div>
			</lay-form-item>
			<lay-checkbox
			value=""
			name="like"
			v-model="remember"
			skin="primary"
			label="1"
			>记住密码</lay-checkbox
			>
			<a
			href="javascript:void(0);"
			@click="openRegister"
			style="display: inline-block; float: right"
			>立即注册</a
			>
			<lay-form-item :label-width="0">
			<lay-button
					style="margin-top: 20px"
					type="primary"
					:loading="loging"
					:fluid="true"
					loadingIcon="layui-icon-loading"
					@click="loginSubmit"
					>登录</lay-button
			>
			</lay-form-item>
   </lay-form>
  </div>
  <registerDialog ref="registerDialogRef"/>
</template>
<script setup lang="ts">
import { login } from '../../../api/module/user'
import { verificationImg } from '../../../api/module/common'
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from './../../../store/user'
import { layer } from '@layui/layer-vue'
import registerDialog  from './registerDialog.vue';

const registerDialogRef = ref();
const router = useRouter()
const userStore = useUserStore()
const verificationImgUrl = ref('')
const loging = ref(false);
const remember = ref(false)
const formRef = ref()
const loginForm = reactive({
  userName: 'admin',
  password: '123456',
  verificationCode: '',
  captchaId: '',
  loginWay: 0
})

const rules = ref({
	userName: {
		required: true,
		message: '用户名不能为空'
	},
	password: {
		required: true,
		message: '密码不能为空'
	},
	verificationCode: {
		required: true,
		message: '验证码不能为空'
	}
})

onMounted(()=>{
  toRefreshImg()
})

const toRefreshImg = async () => {
  let res = await verificationImg();
  if (!res.hasError){
    verificationImgUrl.value = 'data:image/gif;base64,' + res.img;
    loginForm.captchaId = res.captchaId;
  }
}

const loginSubmit = () => {
	formRef.value.validate(async (isValidate: boolean)=>{
		if (!isValidate)
		{
			return false;
		}
		loging.value = true;
		let res = await login(loginForm);
		if (!res.hasError)
		{
			setTimeout(() => {
				loging.value = false;
				layer.msg('登录成功', { icon: 1 }, async () => {
						userStore.token = res.token
						userStore.userInfo = res.userInfo
						await userStore.loadResources()
						router.push('/')
					})
			}, 1000);
		}
		else
		{
			loging.value = false;
			toRefreshImg();
		}
	})
}

const openRegister = () =>{
  registerDialogRef.value.openDialog();
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
