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
                让 国 内 .NET 生 态 充 满 活 力
              </h3>
            </div>
          </div>
          <div class="login-ID">
            <div style="font-size: 22px; margin-bottom: 15px; margin-top: 5px">
              🎯 Sign in
            </div>
            <lay-tab type="brief" v-model="method">
              <lay-tab-item title="用户名密码" id="0">
                <userNameLoginForm />
              </lay-tab-item>
              <lay-tab-item title="邮箱" id="2">
                <emailLoginForm />
              </lay-tab-item>
              <lay-tab-item title="手机号" id="1">
                <phoneNumberLoginForm />
              </lay-tab-item>
            </lay-tab>
            <lay-line style="margin: 34px 0px;">Other login methods</lay-line>
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
    <div class="login-footer">
      <p>
        <span>Released under the </span>
        <a href="/index.html">MIT License</a>.
      </p>
      <p>
        <span>Copyright © 2023-2024 Energetic.NET All Rights Reserved  </span>
        <span>
          <img src="../../assets/beian.png" class="bei-an" />
          <a href="https://beian.mps.gov.cn/#/query/webSearch?code=44030002003079" rel="noreferrer" target="_blank">粤公网安备44030002003079</a>
          &nbsp;&nbsp;<a href="https://beian.miit.gov.cn" target="_blank">鄂ICP备19006397号-1</a>
        </span>
      </p>
      <p>
        鸣谢： 
        <a href="https://gitee.com/vintom/Energetic.NET" style="color:#c71d23;font-weight:800;margin:0px 5px;">Gitee</a>
        <a href="http://www.layui-vue.com/zh-CN/index" style="color:#16baaa;font-weight:800;margin:0px 5px;">layui-vue</a>
        <a href="https://github.com/DreamTom/Energetic.NET" style="color:#9b16ff;font-weight:800;margin:0px 5px;">Github</a>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import userNameLoginForm  from './component/userNameLoginForm.vue'
import phoneNumberLoginForm  from './component/phoneNumberLoginForm.vue'
import emailLoginForm  from './component/emailLoginForm.vue'

const method = ref('0')

</script>
<style scoped>

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
  height: 540px;
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
  padding: 20px 30px;
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

.login-footer {
    position: absolute;
    bottom: 0;
    width: 100%;
    padding: 30px 0;
    line-height: 30px;
    text-align: center;
    /* border-top: 1px solid #eee; */
    color: #3c3c3cb3;
    font-weight: 300;
    font-size: 13.6px;
    /* background: #f9f9f9 */
}

.bei-an{
  display: inline;
   margin: 0 5px;
   width: 16px;
   vertical-align: middle;
   border-style: none;
}
</style>
