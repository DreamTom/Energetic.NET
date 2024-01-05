<!--注册弹窗-->
<template>
  <lay-layer v-model="show" :shadeClose="false" title="用户注册" :btn="registerActions">
    <div style="padding-right: 20px;padding-top: 20px;">
      <lay-form :model="registerModel" ref="layFormRef">
        <lay-form-item label="昵称" prop="nickName" required>
          <lay-input v-model="registerModel.nickName"></lay-input>
        </lay-form-item>
        <lay-form-item label="姓名" prop="realName" :required="false">
          <lay-input v-model="registerModel.realName"></lay-input>
        </lay-form-item>
        <lay-form-item label="性别" prop="gender" required>
          <lay-select v-model="registerModel.gender" placeholder="请选择">
            <lay-select-option :value="0" label="男"></lay-select-option>
            <lay-select-option :value="1" label="女"></lay-select-option>
          </lay-select>
        </lay-form-item>
        <lay-form-item label="用户名" prop="userName" required>
          <lay-input v-model="registerModel.userName"></lay-input>
        </lay-form-item>
        <lay-form-item label="密码" prop="password" required>
          <lay-input v-model="registerModel.password" type="password" password></lay-input>
        </lay-form-item>
        <lay-form-item label="验证码" prop="verificationCode" required>
          <div style="width: 186px;display: inline-block">
            <lay-input :allow-clear="true"
              v-model="registerModel.verificationCode"></lay-input>
          </div>
          <div class="login-captach" @click="toRefreshImg">
            <img style="width: 100%" :src="verificationImgUrl" alt="获取验证码" />
          </div>
        </lay-form-item>
      </lay-form>
    </div>
  </lay-layer>
</template>

<script setup lang="ts">
import { register } from '../../../api/module/user'
import { verificationImg } from '../../../api/module/common'
import { reactive, ref } from 'vue'
import { layer } from '@layui/layer-vue'

const show = ref(false);
const verificationImgUrl = ref('');
const registerModel = reactive({
  userName: '',
  realName: '',
  gender: 0,
  password: '',
  nickName: '',
  verificationCode: '',
  captchaId: ''
})
const layFormRef = ref();

const toRefreshImg = async () => {
  let res = await verificationImg();
  if (!res.hasError){
    verificationImgUrl.value = 'data:image/gif;base64,' + res.img;
    registerModel.captchaId = res.captchaId;
  }
}

const openDialog = ()=>{
  show.value = true;
  toRefreshImg();
}

const registerActions = ref([
  {
      text: "确认",
      callback: () => {
        layFormRef.value.validate(async (isValidate:Boolean)=>{
          if (isValidate){
            var res = await register(registerModel);
            if (!res.hasError){
              layer.msg('注册成功', {icon: 1});
              layFormRef.value.reset();
              show.value = false;
            }else{
              toRefreshImg();
            }
          }
        })
      }
  },
  {
      text: "取消",
      callback: () => {
          show.value = false;
      }
  }
])

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