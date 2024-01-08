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
              <lay-button style="margin-left: 20px" type="normal" size="sm" @click="loadDataSource">
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
        :data-source="dataSource" v-model:selected-keys="selectedKeys" @change="loadDataSource" @sortChange="sortChange">
        <template v-slot:toolbar>
          <lay-button size="sm" type="primary" @click="changeVisible('新增', null)">
            <lay-icon class="layui-icon-addition"></lay-icon>
            新增</lay-button>
        </template>
        <template v-slot:operator="{ row }">
          <lay-button size="xs" border="green" border-style="dashed" @click="changeVisible('编辑', row)">编辑</lay-button>
          <lay-button size="xs" border="blue" border-style="dashed" @click="toPrivileges(row)">分配权限</lay-button>
          <lay-popconfirm content="确定要删除此角色吗?" @confirm="confirm(row.id)" @cancel="cancel">
            <lay-button size="xs" border="red" border-style="dashed">删除</lay-button>
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

    <lay-layer v-model="visible22" title="分配权限" :area="['500px', '450px']">
      <div style="height: 320px; overflow: auto">
        <lay-tree style="margin-left: 40px" :tail-node-icon="false" :data="data2" :showCheckbox="showCheckbox2"
          v-model:checkedKeys="checkedKeys2">
          <template #title="{ data }">
            <lay-icon :class="data.icon"></lay-icon>
            {{ data.title }}
          </template>
        </lay-tree>
      </div>
      <lay-line></lay-line>
      <div style="width: 90%; text-align: right">
        <lay-button size="sm" type="primary" @click="toSubmit">保存</lay-button>
        <lay-button size="sm" @click="toCancel">取消</lay-button>
      </div>
    </lay-layer>
  </lay-container>
</template>
<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { layer } from '@layui/layui-vue'
import { addRole, delRole, editRole, getRoles } from '../../../api/module/role';

const searchQuery = reactive({
  name: '',
  code: '',
  pageNumber: 1,
  pageSize: 10
})
const page = reactive({ current: 1, limit: 10, total: 4 })

const toReset = async () => {
  searchQuery.name = ''
  searchQuery.code = ''
  await loadDataSource()
}

const loading = ref(false)
const selectedKeys = ref()
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

const sortChange = (key: any, sort: number) => {
  layer.msg(`字段${key} - 排序${sort}, 你可以利用 sort-change 实现服务端排序`)
}

const changeStatus = (isChecked: boolean, row: any) => {
  dataSource.value.forEach((item: any) => {
    if (item.id === row.id) {
      layer.msg('Success', { icon: 1 }, () => {
        item.flage = isChecked
      })
    }
  })
}
const remove = () => {
  layer.msg(selectedKeys.value, { area: '50%' })
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
      name: '',
      code: '',
      description: ''
    })
  }
  isVisible.value = !isVisible.value
}
const submit11 = function () {
  layFormRef.value.validate((isValidate: any, model: any, errors: any) => {
    layer.open({
      type: 1,
      title: '表单提交结果',
      content: `<div style="padding: 10px"><p>是否通过 : ${isValidate}</p> <p>表单数据 : ${JSON.stringify(
        model
      )} </p> <p>错误信息 : ${JSON.stringify(errors)}</p></div>`,
      shade: false,
      isHtmlFragment: true,
      btn: [
        {
          text: '确认',
          callback(index: number) {
            layer.close(index)
          }
        }
      ],
      area: '500px'
    })
  })
}
// 清除校验
const clearValidate11 = function () {
  layFormRef.value.clearValidate()
}
// 重置表单
const reset11 = function () {
  layFormRef.value.reset()
}
function toRemove() {
  if (selectedKeys.value.length == 0) {
    layer.msg('您未选择数据，请先选择要删除的数据', { icon: 3, time: 2000 })
    return
  }
  layer.confirm('您将删除所有选中的数据？', {
    title: '提示',
    btn: [
      {
        text: '确定',
        callback: (id: any) => {
          layer.msg('您已成功删除')
          layer.close(id)
        }
      },
      {
        text: '取消',
        callback: (id: any) => {
          layer.msg('您已取消操作')
          layer.close(id)
        }
      }
    ]
  })
}

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
      visible22.value = false
    }
  })
}
function toCancel() {
  isVisible.value = false
  visible22.value = false
}
const confirm = async (id: string) => {
  let res = await delRole(id)
  if (!res.hasError){
    loadDataSource()
  }
}
function cancel() {
  layer.msg('您已取消操作')
}

const visible22 = ref(false)
const checkedKeys2 = ref([])
const showCheckbox2 = ref(true)

const data2 = ref([
  {
    icon: 'layui-icon-home',

    title: '工作空间',
    id: 1,
    checked: true,
    spread: true,
    children: [
      {
        title: '工作台',
        icon: 'layui-icon-util',
        id: 3
      },
      {
        title: '控制台',
        icon: 'layui-icon-engine',
        id: 4,
        spread: true
      },
      {
        title: '分析页',
        id: 20,
        icon: 'layui-icon-chart-screen'
      },
      {
        title: '监控页',
        id: 21,
        icon: 'layui-icon-find-fill'
      }
    ]
  },
  {
    title: '表单页面',
    icon: 'layui-icon-table',
    id: 2,
    spread: true,
    children: [
      {
        title: '基础表单',
        icon: 'layui-icon-form',
        id: 5,
        spread: true
      },
      {
        title: '复杂表单',
        icon: 'layui-icon-form',
        id: 6
      }
    ]
  },
  {
    title: '个人中心',
    icon: 'layui-icon-slider',
    id: 16,
    children: [
      {
        icon: 'layui-icon-username',
        title: '我的资料',
        id: 17,
        fixed: true
      },
      {
        title: '我的消息',
        icon: 'layui-icon-email',
        id: 27
      }
    ]
  },
  {
    title: '系统管理',
    icon: 'layui-icon-set',
    id: 19,
    children: [
      {
        icon: 'layui-icon-user',
        title: '用户管理',
        id: 25,
        fixed: true
      },
      {
        title: '角色管理',
        icon: 'layui-icon-group',
        id: 29
      },
      {
        title: '机构管理',
        icon: 'layui-icon-transfer',
        id: 29
      }
    ]
  }
])

function toPrivileges(row: any) {
  visible22.value = true
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