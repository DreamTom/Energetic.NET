<template>
  <div class="login-wrap">
    <div class="login-root">
      <div class="login-main">
        <img class="login-one-ball"
          src="https://assets.codehub.cn/micro-frontend/login/fca1d5960ccf0dfc8e32719d8a1d80d2.png" />
        <img class="login-two-ball"
          src="https://assets.codehub.cn/micro-frontend/login/4bcf705dad662b33a4fc24aaa67f6234.png" />
        <div class="login-container">
          <div class="login-side">
            <div class="login-bg-title">
              <h1>Energetic.NET</h1>

              <h3 style="margin: 20px auto">
                让 .NET 生 态 更 加 充 满 活 力
              </h3>
            </div>
          </div>
          <div class="login-ID">
            <div style="font-size: 22px; margin-bottom: 15px; margin-top: 5px">
              🎯 Sign in
            </div>
            <lay-tab type="brief" v-model="method">
              <lay-tab-item title="账号密码" id="1">
                <div style="height: 250px">
                  <lay-form-item :label-width="0">
                    <lay-input :allow-clear="true" prefix-icon="layui-icon-username" placeholder="用户名或手机号或邮箱"
                      v-model="loginForm.userName"></lay-input>
                  </lay-form-item>
                  <lay-form-item :label-width="0">
                    <lay-input :allow-clear="true" prefix-icon="layui-icon-password" placeholder="密码" password
                      type="password" v-model="loginForm.password"></lay-input>
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
                  <lay-checkbox value="" name="like" v-model="remember" skin="primary" label="1">记住密码</lay-checkbox>
                  <a href="javascript:void(0);" @click="openRegister" style="display: inline-block;float: right;">注册</a>

                  <lay-form-item :label-width="0">
                    <lay-button style="margin-top: 20px" type="primary" :loading="loging" :fluid="true"
                      loadingIcon="layui-icon-loading" @click="loginSubmit">登录</lay-button>
                  </lay-form-item>
                </div>
              </lay-tab-item>
              <lay-tab-item title="验证码" id="2">
                <div style="width: 200px; height: 250px; margin: 0 auto">
                  <lay-qrcode text="http://www.layui-vue.com" :width="200" color="#000"
                    style="margin: 10px 0 20px"></lay-qrcode>
                  <div style="text-align: center; cursor: pointer" @click="toRefreshQrcode">
                    <lay-icon type="layui-icon-refresh-three"> </lay-icon>
                    刷新二维码
                  </div>
                </div>
              </lay-tab-item>
              <lay-tab-item title="二维码" id="3">
                <div style="width: 200px; height: 250px; margin: 0 auto">
                  <lay-qrcode text="http://www.layui-vue.com" :width="200" color="#000"
                    style="margin: 10px 0 20px"></lay-qrcode>
                  <div style="text-align: center; cursor: pointer" @click="toRefreshQrcode">
                    <lay-icon type="layui-icon-refresh-three"> </lay-icon>
                    刷新二维码
                  </div>
                </div>
              </lay-tab-item>
            </lay-tab>
            <lay-line>Other login methods</lay-line>
            <ul class="other-ways">
              <li>
                <div class="line-container">
                  <img class="icon" src="../../assets/login/w.svg" />
                  <p class="text">微信</p>
                </div>
              </li>
              <li>
                <div class="line-container">
                  <img class="icon" src="../../assets/login/q.svg" />
                  <p class="text">钉钉</p>
                </div>
              </li>
              <li>
                <div class="line-container">
                  <img class="icon" src="../../assets/login/a.svg" />
                  <p class="text">Gitee</p>
                </div>
              </li>
              <li>
                <div class="line-container">
                  <img class="icon" src="../../assets/login/f.svg" />
                  <p class="text">Github</p>
                </div>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!--注册-->
  <lay-layer v-model="showRegister" :shade="false" :title="false" :btn="registerActions">
    <div style="padding-right: 20px;">
      <lay-tab type="brief" v-model="registerMethod">
          <lay-tab-item title="用户名注册" id="0">
            <lay-form :model="registerModel" ref="layFormRef" required>
              <lay-form-item label="昵称" prop="nickName">
                <lay-input v-model="registerModel.nickName"></lay-input>
              </lay-form-item>
              <lay-form-item label="性别" prop="gender">
                <lay-select v-model="registerModel.gender" placeholder="请选择">
                  <lay-select-option :value="0" label="男"></lay-select-option>
                  <lay-select-option :value="1" label="女"></lay-select-option>
                </lay-select>
              </lay-form-item>
              <lay-form-item label="用户名" prop="userName">
                <lay-input v-model="registerModel.userName"></lay-input>
              </lay-form-item>
              <lay-form-item label="密码" prop="password">
                <lay-input v-model="registerModel.password" type="password" password></lay-input>
              </lay-form-item>
            </lay-form>
          </lay-tab-item>
          <lay-tab-item title="手机号注册" id="1">
            <lay-form :model="registerModel" ref="layFormRef" required>
              <lay-form-item label="手机号" prop="phoneNumber">
                <lay-input v-model="registerModel.phoneNumber"></lay-input>
              </lay-form-item>
              <lay-form-item label="验证码" prop="verificationCode" mode="inline">
                <lay-input style="width: 120px;" v-model="registerModel.verificationCode"></lay-input>
                <lay-button style="margin-left: 5px;" @click="send(1)">发送</lay-button>
              </lay-form-item>
            </lay-form>
          </lay-tab-item>
          <lay-tab-item title="邮箱注册" id="2">
            <lay-form :model="registerModel" ref="layFormRef" required>
              <lay-form-item label="邮箱" prop="emailAddress">
                <lay-input v-model="registerModel.emailAddress"></lay-input>
              </lay-form-item>
              <lay-form-item label="验证码" prop="verificationCode" mode="inline">
                <lay-input style="width: 120px;" v-model="registerModel.verificationCode"></lay-input>
                <lay-button style="margin-left: 5px;" @click="send(2)">发送</lay-button>
              </lay-form-item>
            </lay-form>
          </lay-tab-item>
      </lay-tab>
    </div>
  </lay-layer>
</template>

<script setup lang="ts">
import { login, register } from '../../api/module/user'
import { verificationImg, loginQrcode } from '../../api/module/common'
import { onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '../../store/user'
import { layer } from '@layui/layer-vue'

const router = useRouter()
const userStore = useUserStore()
const method = ref('1')
const registerMethod = ref('0');
const verificationImgUrl = ref('')
const loging = ref(false);
const loginQrcodeText = ref('')
const remember = ref(false)
const loginForm = reactive({
  userName: 'admin',
  password: '123456',
  verificationCode: '',
  captchaId: '',
  loginWay: 0
})
const showRegister = ref(false);
const registerModel = reactive({
  userName: '',
  password: '',
  nickName: '',
  phoneNumber: '',
  emailAddress: '',
  verificationCode: '',
  gender: 0,
  registerWay: 0
})
const layFormRef = ref();

onMounted(() => {
  toRefreshImg()
  // toRefreshQrcode()
})

const loginSubmit = async () => {
  loging.value = true;
  let res = await login(loginForm);
  if (res)
  {
    setTimeout(() => {
      loging.value = false;
      layer.msg('登录成功', { icon: 1 }, async () => {
          userStore.token = res.token
          await userStore.loadMenus()
          await userStore.loadPermissions()
          router.push('/')
        })
    }, 1000);
  }
  else
  {
    loging.value = false;
    toRefreshImg();
  }
}

const toRefreshImg = async () => {
  let res = await verificationImg();
  if (res){
    verificationImgUrl.value = 'data:image/gif;base64,' + res.img;
    loginForm.captchaId = res.captchaId;
  }
}

const toRefreshQrcode = async () => {
  let { data, code, msg } = await loginQrcode()
  if (code == 200) {
    loginQrcodeText.value = data.data
  } else {
    layer.msg(msg, { icon: 2 })
  }
}

const openRegister = () =>{
  showRegister.value = true;
}

const send = async (type: number) =>{
  
}

const registerActions = ref([
  {
      text: "确认",
      callback: async () => {
        var res = await register(registerModel);
        if (res){
          layer.msg('注册成功', {icon: 1});
          showRegister.value = false;
        }
      }
  },
  {
      text: "取消",
      callback: () => {
          showRegister.value = false;
      }
  }
])

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

.login-one-ball {
  opacity: 0.4;
  position: absolute;
  max-width: 568px;
  left: -400px;
  bottom: 0px;
}

.login-two-ball {
  opacity: 0.4;
  position: absolute;
  max-width: 320px;
  right: -200px;
  top: -60px;
}

.login-wrap {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  overflow: auto;
  min-width: 600px;
  z-index: 9;
  background-image: url(https://assets.codehub.cn/micro-frontend/login/f7eeecbeccefe963298c23b54741d473.png);
  background-repeat: no-repeat;
  background-size: cover;
  min-height: 100vh;
}

.login-wrap :deep(.layui-input-block) {
  margin-left: 0 !important;
}

.login-root {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  display: flex;
  justify-content: center;
  width: 100%;
  min-width: 320px;
  background-color: initial;
}

.login-main {
  position: relative;
  display: block;
}

.logo-container {
  max-width: calc(100vw - 28px);
  margin-bottom: 40px;
  text-align: center;
  display: flex;
  align-items: center;
  justify-content: center;
}

.logo-container .logo {
  display: inline-block;
  height: 30px;
  width: 143px;
  background: url() no-repeat 50%;
  background-size: contain;
  cursor: pointer;
}

.login-container {
  position: relative;
  overflow: hidden;
  width: 940px;
  height: 520px;
  max-width: calc(100vw - 28px);
  border-radius: 4px;
  background: hsla(0, 0%, 100%, 0.5);
  backdrop-filter: blur(30px);
  display: flex;
  box-shadow: 6px 6px 12px 4px rgba(0, 0, 0, 0.1);
}

.login-side {
  padding: 40px 20px 20px;
  background-color: var(--global-primary-color);
  flex: 1;
  height: 100%;
}

.login-bg-title {
  flex: 1;
  height: 84%;
  color: #fff;
  text-align: center;
  background-image: url('../../assets/login/login-bg.svg');
  background-repeat: no-repeat;
  background-position: bottom;
  background-size: contain;
  text-align: center;
  min-width: 200px;
}

.login-ID {
  padding: 20px;
  width: 380px;
  min-width: 380px;
}

.login-container .layui-tab-head {
  background: transparent;
}

.login-container .layui-input-wrapper {
  margin-top: 10px;
  margin-bottom: 10px;
}

.login-container .layui-input-wrapper {
  margin-top: 12px;
  margin-bottom: 12px;
}

.login-container .assist {
  margin-top: 5px;
  margin-bottom: 5px;
  letter-spacing: 2px;
}

.login-container .layui-btn {
  margin: 10px 0px 10px 0px;
  letter-spacing: 2px;
  height: 40px;
}

.login-container .layui-line-horizontal {
  letter-spacing: 2px;
  margin-bottom: 34px;
  margin-top: 24px;
}

.other-ways {
  display: flex;
  justify-content: space-between;
  margin: 0;
  padding: 0;
  list-style: none;
  font-size: 14px;
  font-weight: 400;
}

.other-ways li {
  width: 100%;
}

.line-container {
  justify-content: center;
  align-items: center;
  text-align: center;
  cursor: pointer;
}

.line-container .icon {
  height: 28px;
  width: 28px;
  margin-right: 0;
  vertical-align: middle;
  border-radius: 50%;
  background: #fff;
  box-shadow: 0 1px 2px 0 rgb(9 30 66 / 4%), 0 1px 4px 0 rgb(9 30 66 / 10%),
    0 0 1px 0 rgb(9 30 66 / 10%);
}

.line-container .text {
  display: block;
  margin: 12px 0 0;
  font-size: 12px;
  color: #8592a6;
}

:deep(.layui-tab-title .layui-this) {
  background-color: transparent;
}
</style>
