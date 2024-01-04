<!--注册弹窗-->
<template>
  <lay-layer v-model="show" :shadeClose="false" :title="false" :btn="registerActions">
    <div style="padding-right: 20px;">
      <lay-tab type="brief" v-model="registerMethod">
          <lay-tab-item title="用户名注册" id="0">
            <lay-form :model="userNameRegisterModel" ref="layFormRef" required>
              <lay-form-item label="昵称" prop="nickName">
                <lay-input v-model="userNameRegisterModel.nickName"></lay-input>
              </lay-form-item>
              <lay-form-item label="性别" prop="gender">
                <lay-select v-model="registerModel.gender" placeholder="请选择">
                  <lay-select-option :value="0" label="男"></lay-select-option>
                  <lay-select-option :value="1" label="女"></lay-select-option>
                </lay-select>
              </lay-form-item>
              <lay-form-item label="用户名" prop="userName">
                <lay-input v-model="userNameRegisterModel.userName"></lay-input>
              </lay-form-item>
              <lay-form-item label="密码" prop="password">
                <lay-input v-model="userNameRegisterModel.password" type="password" password></lay-input>
              </lay-form-item>
              <lay-form-item label="验证码" prop="verificationCode">
                <div style="width: 186px;display: inline-block">
                  <lay-input :allow-clear="true"
                    v-model="userNameRegisterModel.verificationCode"></lay-input>
                </div>
                <div class="login-captach" @click="toRefreshImg">
                  <img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
                </div>
              </lay-form-item>
            </lay-form>
          </lay-tab-item>
          <lay-tab-item title="手机号注册" id="1">
            <lay-form :model="phoneNumberRegisterModel" ref="layFormRef1" required>
              <lay-form-item label="手机号" prop="phoneNumber">
                <lay-input v-model="phoneNumberRegisterModel.phoneNumber"></lay-input>
              </lay-form-item>
              <lay-form-item label="验证码" prop="verificationCode">
                <div style="width: 186px;display: inline-block">
                  <lay-input :allow-clear="true"
                    v-model="phoneNumberRegisterModel.verificationCode"></lay-input>
                </div>
                <div class="login-captach" @click="toRefreshImg">
                  <img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
                </div>
              </lay-form-item>
              <lay-form-item label="短信验证码" prop="secondCode">
                <lay-input v-model="phoneNumberRegisterModel.secondCode">
                  <template #append><span @click="send(1)" style="cursor: pointer;">发送验证码</span></template>
                </lay-input>
              </lay-form-item>
            </lay-form>
          </lay-tab-item>
          <lay-tab-item title="邮箱注册" id="2">
            <lay-form :model="emailRegisterModel" ref="layFormRef2" required>
              <lay-form-item label="邮箱" prop="emailAddress">
                <lay-input v-model="emailRegisterModel.emailAddress"></lay-input>
              </lay-form-item>
              <lay-form-item label="验证码" prop="verificationCode">
                <div style="width: 186px;display: inline-block">
                  <lay-input :allow-clear="true"
                    v-model="emailRegisterModel.verificationCode"></lay-input>
                </div>
                <div class="login-captach" @click="toRefreshImg">
                  <img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
                </div>
              </lay-form-item>
              <lay-form-item label="邮箱验证码" prop="secondCode">
                <lay-input v-model="emailRegisterModel.secondCode">
                  <template #append><span @click="send(2)" style="cursor: pointer;">发送验证码</span></template>
                </lay-input>
              </lay-form-item>
            </lay-form>
          </lay-tab-item>
      </lay-tab>
    </div>
  </lay-layer>
</template>

<script setup lang="ts">
import { register } from '../../../api/module/user'
import { verificationImg } from '../../../api/module/common'
import { reactive, ref } from 'vue'
import { layer } from '@layui/layer-vue'

const show = ref(false);
const registerMethod = ref('0');
const verificationImgUrl = ref('');
const userNameRegisterModel = reactive({
  userName: '',
  gender: 0,
  password: '',
  nickName: '',
  verificationCode: '',
  captchaId: '',
  registerWay: 0
})
const phoneNumberRegisterModel = reactive({
  phoneNumber: '',
  verificationCode: '',
  captchaId: '',
  secondCode: '',
  registerWay: 1
})
const emailRegisterModel = reactive({
  emailAddress: '',
  verificationCode: '',
  captchaId: '',
  secondCode: '',
  registerWay: 2
})
const registerInfo = () => ({
  userName: '',
  password: '',
  nickName: '',
  emailAddress: '',
  phoneNumber: '',
  verificationCode: '',
  captchaId: '',
  secondCode: '',
  gender: 0,
  registerWay: 0
})
const registerModel = reactive(registerInfo())
let captchaId = ''
const layFormRef = ref();
const layFormRef1 = ref();
const layFormRef2 = ref();

const toRefreshImg = async () => {
  let res = await verificationImg();
  if (!res.hasError){
    verificationImgUrl.value = 'data:image/gif;base64,' + res.img;
    captchaId = res.captchaId;
  }
}

const send = async (type: number) =>{
  
}

const openDialog = ()=>{
  show.value = true;
  toRefreshImg();
}

const registerActions = ref([
  {
      text: "确认",
      callback: () => {
        const regWay = Number(registerMethod.value);
        if (regWay == 0){
          layFormRef.value.validate(async (isValidate: Boolean)=>{
          if(isValidate){
            userNameRegisterModel.captchaId = captchaId;
            await reg(userNameRegisterModel);
          }
          })
        } else if(regWay == 1){
          layFormRef1.value.validate(async (isValidate: Boolean)=>{
          if(isValidate){
            phoneNumberRegisterModel.captchaId = captchaId;
            await reg(phoneNumberRegisterModel);
          }
          })
        } else{
          layFormRef2.value.validate(async (isValidate: Boolean)=>{
          if(isValidate){
            emailRegisterModel.captchaId = captchaId;
            await reg(emailRegisterModel);
          }
          })
        }
      }
  },
  {
      text: "取消",
      callback: () => {
          show.value = false;
      }
  }
])

const reg = async (regModel: any) => {
  var res = await register(regModel);
  if (!res.hasError){
    layer.msg('注册成功', {icon: 1});
    layFormRef.value.reset();
    layFormRef1.value.reset();
    layFormRef2.value.reset();
    show.value = false;
  }else{
    toRefreshImg();
  }
}

defineExpose({
  openDialog
})

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