<template>
  <lay-container fluid="true" class="role-box">
    <lay-card>
      <lay-form style="margin-top: 10px">
        <lay-row>
          <lay-col :md="5">
            <lay-form-item label="角色名称" label-width="80" prop="name">
              <lay-input v-model="searchQuery.name" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label="角色标识" label-width="80" prop="code">
              <lay-input v-model="searchQuery.code" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label-width="20">
              <lay-button v-permission="['sys:roles:query']" style="margin-left: 20px" type="normal" size="sm" @click="loadDataSource">
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
        <template v-slot:toolbar>
          <lay-button size="sm" type="primary" @click="changeVisible('新增', null)">
            <lay-icon class="layui-icon-addition"></lay-icon>
            新增</lay-button>
        </template>
        <template v-slot:operator="{ row }">
          <lay-button v-permission="['sys:roles:edit']" size="xs" border="green" border-style="dashed" @click="changeVisible('编辑', row)">编辑</lay-button>
          <lay-button v-permission="['sys:roles:auth','sys:roles:queryAuth']" size="xs" border="blue" border-style="dashed" @click="toPrivileges(row)">分配权限</lay-button>
          <lay-popconfirm content="确定要删除此角色吗?" @confirm="confirm(row.id)">
            <lay-button v-permission="['sys:roles:delete']" size="xs" border="red" border-style="dashed">删除</lay-button>
          </lay-popconfirm>
        </template>
      </lay-table>
    </div>

    <lay-layer v-model="isVisible" :title="title" :shadeClose="false" :area="['500px', '370px']">
      <div style="padding: 20px">
        <lay-form :model="roleModel" ref="layFormRef">
          <lay-form-item label="角色名称" prop="name" required>
            <lay-input v-model="roleModel.name"></lay-input>
          </lay-form-item>
          <lay-form-item label="角色标识" prop="code" required>
            <lay-input v-model="roleModel.code"></lay-input>
          </lay-form-item>
          <lay-form-item label="描述" prop="remark">
            <lay-textarea placeholder="请输入描述" v-model="roleModel.description"></lay-textarea>
          </lay-form-item>
        </lay-form>
        <div style="width: 100%; text-align: center">
          <lay-button size="sm" type="primary" @click="toSubmit">保存</lay-button>
          <lay-button size="sm" @click="toCancel">取消</lay-button>
        </div>
      </div>
    </lay-layer>

    <lay-layer v-model="permissionVisible" title="分配权限" :shadeClose="false" :area="['500px', '450px']">
      <div style="height: 320px; overflow: auto">
        <lay-tree style="margin-left: 40px" :tail-node-icon="false" :data="resources" :showCheckbox="true"
          v-model:checkedKeys="checkedResourceIds">
          <template #title="{ data }">
            <lay-icon :class="data.icon"></lay-icon>
            {{ data.title }}
          </template>
        </lay-tree>
      </div>
      <lay-line></lay-line>
      <div style="width: 90%; text-align: right">
        <lay-button size="sm" type="primary" @click="toSubmitPermission">保存</lay-button>
        <lay-button size="sm" @click="toCancel">取消</lay-button>
      </div>
    </lay-layer>
  </lay-container>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { layer } from '@layui/layui-vue'
import { addRole, delRole, editRole, getRoles, getRoleResourceIds,editRoleResource } from '../../../api/module/role';
import { getMenuTree } from '../../../api/module/resource';

const searchQuery = reactive({
  name: '',
  code: '',
  pageNumber: 1,
  pageSize: 10,
  orderBy: '',
  propName: ''
})
const page = reactive({ current: 1, limit: 10, total: 4 })

const toReset = async () => {
  searchQuery.name = ''
  searchQuery.code = ''
  await loadDataSource()
}

const loading = ref(false)
const columns = ref([
  { title: '序号', width: '30px', type: 'number', fixed: 'left' },
  { title: '角色名称', width: '80px', key: 'name' },
  { title: '角色标识', width: '80px', key: 'code' },
  { title: '备注', width: '260px', key: 'description' },
  { title: '创建人', width: '120px', key: 'createdBy', sort: 'desc' },
  { title: '创建时间', width: '120px', key: 'createdTime', sort: 'desc' },
  {
    title: '操作',
    width: '150px',
    customSlot: 'operator',
    key: 'operator',
    fixed: 'right'
  }
])
const dataSource = ref([])

onMounted(async () => {
  await loadDataSource();
  await loadMenuTree();
})

const loadDataSource = async () => {
  loading.value = true;
  searchQuery.pageNumber = page.current;
  searchQuery.pageSize = page.limit;
  let res = await getRoles(searchQuery);
  if (!res.hasError) {
    dataSource.value = res.items;
    page.total = res.totalCount;
    loading.value = false;
  } else {
    loading.value = false;
  }
}

const resources = ref([])
const loadMenuTree = async () => {
  let res = await getMenuTree(false)
  if (!res.hasError){
    resources.value = res;
  }
}

const sortChange = async (key: any, sort: string) => {
  searchQuery.orderBy = sort
  searchQuery.propName = key
  await loadDataSource()
}

const roleModel = reactive({
  name: '',
  code: '',
  description: '',
  id: ''
})
const layFormRef = ref()
const isVisible = ref(false)

const title = ref('新增')
const changeVisible = (text: any, row: any) => {
  title.value = text
  if (row != null) {
    let info = JSON.parse(JSON.stringify(row))
    Object.assign(roleModel, info);
  } else {
    Object.assign(roleModel, {
      id: '',
      name: '',
      code: '',
      description: ''
    })
  }
  isVisible.value = !isVisible.value
}

//修改角色权限
const toSubmitPermission = async () =>{
  let res = await editRoleResource(roleId.value, checkedResourceIds.value);
  if (!res.hasError){
    layer.msg('保存成功！', { icon: 1, time: 1000 })
    permissionVisible.value = false
  }
}

//修改角色信息
const toSubmit = () => {
  layFormRef.value.validate(async (isValidate: boolean) => {
    if (!isValidate)
      return false
    let res;
    if (roleModel.id == '') {
      res = await addRole(roleModel);
    } else {
      res = await editRole(roleModel);
    }
    if (!res.hasError) {
      layer.msg('保存成功！', { icon: 1, time: 1000 })
      isVisible.value = false
      await loadDataSource()
      isVisible.value = false
    }
  })
}

// 关闭角色权限弹窗
function toCancel() {
  isVisible.value = false
  permissionVisible.value = false
}


const confirm = async (id: string) => {
  let res = await delRole(id)
  if (!res.hasError){
    layer.msg('删除成功！', { icon: 1, time: 1000 })
    loadDataSource()
  }
}

const permissionVisible = ref(false)
const checkedResourceIds = ref([])

const roleId = ref('');

// 打开角色授权页面
const toPrivileges = async (row: any) => {
  roleId.value = row.id;
  let res = await getRoleResourceIds(row.id);
  if (!res.hasError){
    checkedResourceIds.value = res;
  }
  permissionVisible.value = true
}

</script>

<style scoped>
.role-box {
  width: calc(100vw - 220px);
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