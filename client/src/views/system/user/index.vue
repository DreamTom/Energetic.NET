<template>
  <lay-container fluid="true" class="user-box">
    <lay-card>
      <lay-form style="margin-top: 10px">
        <lay-row>
          <lay-col :md="5">
            <lay-form-item label="昵称" label-width="80">
              <lay-input v-model="searchQuery.nickName" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label="用户名" label-width="80">
              <lay-input v-model="searchQuery.userName" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label="性别" label-width="80">
              <lay-select class="search-input" size="sm" v-model="searchQuery.gender" :allow-clear="true"
                placeholder="请选择">
                <lay-select-option :value="0" label="男"></lay-select-option>
                <lay-select-option :value="1" label="女"></lay-select-option>
              </lay-select>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label-width="20">
              <lay-button v-permission="['sys:users:query']" style="margin-left: 20px" type="normal" size="sm" @click="loadDataSource">
                查询
              </lay-button>
              <lay-button size="sm" @click="toReset"> 重置 </lay-button>
            </lay-form-item>
          </lay-col>
        </lay-row>
      </lay-form>
    </lay-card>
    <!-- table -->
    <div class="table-box">
      <lay-table :page="page" :height="'100%'" :columns="columns" :loading="loading" :default-toolbar="true"
        :data-source="dataSource" @change="loadDataSource" @sortChange="sortChange">
        <template #isEnable="{ row }">
          <lay-switch :model-value="row.isEnable"></lay-switch>
        </template>
        <template #role="{ row }">
          <lay-tag style="margin-left: 5px;" v-for="role in row.roles" color="#F5319D" variant="light">{{ role.name }}</lay-tag>
        </template>
        <template #gender="{ row }">
          {{ row.gender == 0 ? '男' : '女' }}
        </template>
        <template #avatar="{ row }">
          <lay-avatar v-if="row.avatarId" :src="getFileUrl(row.avatarId)"></lay-avatar>
          <lay-avatar v-else> {{ row.firstLetterName }}</lay-avatar>
        </template>
        <template v-slot:toolbar>
          <lay-button v-permission="['sys:users:add']" size="sm" type="primary" @click="changeVisible('新增')">
            <lay-icon class="layui-icon-addition"></lay-icon>
            新增
          </lay-button>
          <lay-button size="sm" @click="toImport">
            <lay-icon class="layui-icon-upload-drag"></lay-icon>
            导入
          </lay-button>
        </template>
        <template v-slot:operator="{ row }">
          <lay-button v-permission="['sys:users:edit']" size="xs" border="green" border-style="dashed" @click="changeVisible('编辑', row)">编辑</lay-button>
          <lay-popconfirm content="确定要删除此用户吗?" @confirm="confirm(row.id)" @cancel="cancel">
            <lay-button v-permission="['sys:users:delete']" size="xs" border="red" border-style="dashed">删除</lay-button>
          </lay-popconfirm>
        </template>
      </lay-table>
    </div>

    <lay-layer v-model="isVisible" :title="title" :area="['500px', '560px']">
      <div style="padding: 20px">
        <lay-form :model="userModel" ref="layFormRef">
          <lay-form-item label="昵称" prop="nickName" required>
            <lay-input v-model="userModel.nickName"></lay-input>
          </lay-form-item>
          <lay-form-item label="姓名" prop="realName">
            <lay-input v-model="userModel.realName"></lay-input>
          </lay-form-item>
          <lay-form-item label="用户名" prop="userName" required>
            <lay-input v-model="userModel.userName"></lay-input>
          </lay-form-item>
          <lay-form-item label="角色" prop="roleIds">
            <lay-select v-model="userModel.roleIds" style="width: 100%" multiple allow-clear placeholder="请选择">
              <lay-select-option v-for="role in roles" :value="role.value" :label="role.label"></lay-select-option>
            </lay-select>
          </lay-form-item>
          <lay-form-item label="性别" prop="gender" required>
            <lay-select v-model="userModel.gender" style="width: 100%">
              <lay-select-option :value="0" label="男"></lay-select-option>
              <lay-select-option :value="1" label="女"></lay-select-option>
            </lay-select>
          </lay-form-item>
          <lay-form-item label="头像" prop="avatarId">
            <div v-show="userModel.avatarId" style="display: inline-block;padding-right: 10px;">
              <img :src="getFileUrl(userModel.avatarId)" style="width: 64px;height: 64px;cursor: pointer;" />
              <lay-icon @click="removeFile(userModel.avatarId)" style="font-size: 20px;position:absolute;left:54px;top:-10px;" color="#ff0000" type="layui-icon-close"></lay-icon>
            </div>
            <lay-upload text="选择图片" @done="uploadFile" :url="uploadImageUrl" field="image">
            </lay-upload>
          </lay-form-item>
          <lay-form-item label="启用" prop="isEnable" required>
            <lay-switch :model-value="userModel.isEnable"></lay-switch>
          </lay-form-item>
        </lay-form>
        <div style="width: 100%; text-align: center">
          <lay-button size="sm" type="primary" @click="toSubmit">保存</lay-button>
          <lay-button size="sm" @click="toCancel">取消</lay-button>
        </div>
      </div>
    </lay-layer>
    <lay-layer v-model="visibleImport" title="导入用户" :area="['380px', '380px']">
      <lay-upload :beforeUpload="beforeUpload10" style="margin: 60px"
        url="https://www.mocky.io/v2/5cc8019d300000980a055e76" v-model="file1" field="file" :auto="false" :drag="true">
        <template #preview>
          {{ file1[0]?.name }}
        </template>
      </lay-upload>
      <div style="width: 100%; text-align: center">
        只能上传小于1000KB的文件
      </div>
    </lay-layer>
  </lay-container>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { layer } from '@layui/layui-vue'
import { getUsers, addUser, editUser, delUser } from '../../../api/module/user';
import { getRoleDpList } from '../../../api/module/role';
import { uploadImageUrl, getFileUrl, delFile  } from '../../../api/module/file';

const searchQuery = reactive({
  userName: '',
  gender: null,
  nickName: '',
  pageNumber: 1,
  pageSize: 20,
  orderBy: '',
  propName: ''
})

const roles = ref<any>([])

const visibleImport = ref(false)
const file1 = ref<any>([])
function toImport() {
  visibleImport.value = true
}
const toReset = async () => {
  searchQuery.userName = ''
  searchQuery.gender = null
  searchQuery.nickName = ''
  await loadDataSource()
}

onMounted(async () => {
  await loadDataSource()
  await loadRoles()
})

const loadRoles = async () => {
  let res = await getRoleDpList()
  if (!res.hasError) {
    roles.value = res;
  }
}

const loading = ref(false)
const page = reactive({ current: 1, limit: 10, total: 100 })
const columns = ref([
  { title: '序号', width: '30px', type: 'number', fixed: 'left' },
  { title: '用户名', width: '120px', key: 'userName', sort: 'desc' },
  { title: '昵称', width: '120px', key: 'nickName', sort: 'desc' },
  { title: '头像', width: '50px', key: 'avatar', customSlot: 'avatar' },
  { title: '姓名', width: '80px', key: 'realName', sort: 'desc' },
  { title: '性别', width: '80px', key: 'gender', customSlot: 'gender' },
  { title: '角色', width: '200px', key: 'roles', customSlot: 'role' },
  { title: '状态', width: '80px', key: 'isEnable', customSlot: 'isEnable' },
  { title: '创建时间', width: '120px', key: 'createdTime', sort: 'desc' },
  { title: '修改时间', width: '120px', key: 'updatedTime', sort: 'desc' },
  {
    title: '操作',
    width: '120px',
    customSlot: 'operator',
    key: 'operator',
    fixed: 'right'
  }
])

const sortChange = async (key: any, sort: string) => {
  searchQuery.orderBy = sort
  searchQuery.propName = key
  await loadDataSource()
}

const dataSource = ref([])

const loadDataSource = async () => {
  loading.value = true;
  searchQuery.pageNumber = page.current;
  searchQuery.pageSize = page.limit;
  let res = await getUsers(searchQuery);
  if (!res.hasError) {
    dataSource.value = res.items;
    page.total = res.totalCount;
    loading.value = false;
  } else {
    loading.value = false;
  }
}

const userModel = reactive({
  userName: '',
  gender: 0,
  nickName: '',
  realName: '',
  isEnable: true,
  avatarId: <any>null,
  roleIds: [],
  id: ''
})
const layFormRef = ref()
const isVisible = ref(false)
const title = ref('新增')
const changeVisible = (text: any, row?: any) => {
  title.value = text
  if (row) {
    let info = JSON.parse(JSON.stringify(row))
    Object.assign(userModel, info);
  } else {
    Object.assign(userModel, {
      userName: '',
      gender: 0,
      nickName: '',
      realName: '',
      isEnable: true,
      avatarId: null,
      roleIds: [],
      id: ''
    })
  }
  isVisible.value = !isVisible.value
}

function toSubmit() {
  layFormRef.value.validate(async (isValidate: boolean) => {
    if (!isValidate)
      return false
    let res = userModel.id == '' ? await addUser(userModel) : await editUser(userModel)
    if (!res.hasError) {
      layer.msg('保存成功！', { icon: 1, time: 1000 })
      isVisible.value = false
      await loadDataSource()
    }
  })
}

function toCancel() {
  isVisible.value = false
}

const confirm = async (id: string) => {
  let res = await delUser(id);
  if (res){
    layer.msg('删除成功！', { icon: 1, time: 1000 })
    await loadDataSource()
  }
}

function cancel() {
  layer.msg('您已取消操作')
}

const uploadFile = (file: any) =>{
  let res = JSON.parse(file.data);
  userModel.avatarId = res.id
}

const removeFile = async (id: string) => {
  let res = await delFile(id)
  if (!res.hasError){
    userModel.avatarId = null
  }
}

const beforeUpload10 = (file: File) => {
  var isOver = false
  if (file.size > 1000) {
    isOver = true
    layer.msg(`file size over 1000 KB`, { icon: 2 })
  }
  return new Promise((resolver) => resolver(true))
}
</script>

<style scoped>
.user-box {
  height: calc(100vh - 110px);
  margin-top: 10px;
  box-sizing: border-box;
  overflow: hidden;
}

.top-search {
  margin-top: 10px;
  padding: 10px;
  height: 40px;
  border-radius: 4px;
  background-color: #fff;
}

.table-box {
  margin-top: 10px;
  padding: 10px;
  height: 700px;
  width: 100%;
  border-radius: 4px;
  box-sizing: border-box;
  background-color: #fff;
}

.search-input {
  display: inline-block;
  width: 98%;
  margin-right: 10px;
}

.isChecked {
  display: inline-block;
  background-color: #e8f1ff;
  color: red;
}
</style>