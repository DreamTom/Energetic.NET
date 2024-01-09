<template>
  <lay-container fluid="true" class="menu-box">
    <lay-card>
      <lay-form style="margin-top: 10px">
        <lay-row>
          <lay-col :md="5">
            <lay-form-item label="菜单名称" label-width="80">
              <lay-input v-model="searchQuery.name" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label="路由地址" label-width="80">
              <lay-input v-model="searchQuery.routePath" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label="权限标识" label-width="80">
              <lay-input v-model="searchQuery.code" placeholder="请输入" size="sm" :allow-clear="true"
                style="width: 98%"></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label-width="20">
              <lay-button style="margin-left: 20px" type="normal" size="sm" @click="toSearch">
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
      <lay-table :height="`100%`" :loading="loading" children-column-name="children" :columns="columns"
        :data-source="dataSource" :default-toolbar="true" :expand-index="1">
        <template #toolbar>
          <lay-button size="sm" @click="changeVisible('新建', null)" type="primary">
            <lay-icon class="layui-icon-addition"></lay-icon>
            新增
          </lay-button>
        </template>
        <template #name="{ row }">
          <lay-icon :class="row.icon"></lay-icon> &nbsp;&nbsp;
          {{ row.name }}
        </template>
        <template #option="{ row }">
          <lay-button v-show="row.type != 3" @click="changeVisible('新建', row)" size="xs" border="blue" border-style="dashed">
            添加
          </lay-button>
          <lay-button @click="changeVisible('修改', row)" size="xs" border="green" border-style="dashed">
            修改
          </lay-button>
          <lay-button v-show="!row.children" @click="toRemove(row.id)" size="xs" border="red" border-style="dashed">
            删除
          </lay-button>
        </template>
        <template #isEnable="{ row }">
          <lay-tag color="#16b777" variant="light" v-if="row.isEnable">启用</lay-tag>
          <lay-tag color="#FF5722" variant="light" v-else>禁用</lay-tag>
        </template>
        <template #type="{ row }">
          <div v-show="row.type == 1">
            <lay-tag color="#165DFF" variant="light">目录</lay-tag>
          </div>
          <div v-show="row.type == 2">
            <lay-tag color="#2dc570" variant="light">菜单</lay-tag>
          </div>
          <div v-show="row.type == 3">
            <lay-tag variant="light">按钮</lay-tag>
          </div>
          <div v-show="row.type == 4">
            <lay-tag color="#F5319D" variant="light">外链</lay-tag>
          </div>
        </template>
      </lay-table>
    </div>

    <!-- layer -->
    <lay-layer v-model="isVisible" :shadeClose="false" :title="title">
      <div style="padding: 20px">
        <lay-form :model="resourceModel" ref="layFormRef">
          <lay-row>
            <lay-col>
              <lay-form-item label="菜单类型" prop="type" required>
                <lay-radio-group name="action" v-model="resourceModel.type">
                  <lay-radio :value="1">目录</lay-radio>
                  <lay-radio :value="2">菜单</lay-radio>
                  <lay-radio :value="3">按钮</lay-radio>
                </lay-radio-group>
              </lay-form-item>
            </lay-col>
          </lay-row>
          <lay-row>
            <lay-col>
              <lay-form-item label="上级菜单" prop="releationIds" :required="resourceModel.type == 3">
                <lay-cascader :options="options" v-model="resourceModel.releationIds" :changeOnSelect="true" placeholder="请选择上级菜单"
                  allow-clear></lay-cascader>
              </lay-form-item>
            </lay-col>
          </lay-row>
          <lay-row>
            <lay-col md="12">
              <lay-form-item label="菜单名称" prop="name" required>
                <lay-input v-model="resourceModel.name"></lay-input>
              </lay-form-item>
              <lay-form-item v-show="resourceModel.type != 3" label="路由路径" prop="routePath" required>
                <lay-input v-model="resourceModel.routePath"></lay-input>
              </lay-form-item>
              <lay-form-item v-show="resourceModel.type == 3" label="接口地址" prop="apiUrl" required>
                <lay-input v-model="resourceModel.apiUrl"></lay-input>
              </lay-form-item>
              <lay-form-item label="是否启用" prop="isEnable" required>
                <lay-checkbox value="1" skin="primary" v-model="resourceModel.isEnable"></lay-checkbox>
              </lay-form-item>
            </lay-col>
            <lay-col md="12">
              <lay-form-item label="排序" prop="displayOrder" required>
                <lay-input-number style="width: 100%" v-model="resourceModel.displayOrder"
                  position="right"></lay-input-number>
              </lay-form-item>
              <lay-form-item v-show="resourceModel.type != 3" label="图标" prop="icon">
                <lay-icon-picker v-model="resourceModel.icon" type="layui-icon-face-smile" page
                  showSearch></lay-icon-picker>
              </lay-form-item>
              <lay-form-item v-show="resourceModel.type == 3" label="请求方法" prop="requestMethod" required>
                <lay-select v-model="resourceModel.requestMethod" placeholder="请选择">
                  <lay-select-option :value="1" label="GET"></lay-select-option>
                  <lay-select-option :value="2" label="POST"></lay-select-option>
                  <lay-select-option :value="3" label="PUT"></lay-select-option>
                  <lay-select-option :value="4" label="DELETE"></lay-select-option>
                </lay-select>
              </lay-form-item>
              <lay-form-item v-show="resourceModel.type == 3" label="权限标识" prop="code" required>
                <lay-input v-model="resourceModel.code"></lay-input>
              </lay-form-item>
            </lay-col>
          </lay-row>
        </lay-form>
        <div style="width: 97%; text-align: right">
          <lay-button size="sm" type="primary" @click="toSubmit">保存</lay-button>
          <lay-button size="sm" @click="toCancel">取消</lay-button>
        </div>
      </div>
    </lay-layer>
  </lay-container>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { layer } from '@layui/layui-vue'
import { getResourceTree, getMenuTree, addResource, editResource, delResource } from '../../../api/module/resource'

const loading = ref(false)
const dataSource = ref([]);
const searchQuery = ref({
  name: '',
  code: '',
  routePath: ''
})

const columns = [
  {
    fixed: 'left',
    type: 'checkbox',
    title: '复选'
  },

  {
    title: '菜单名称',
    key: 'name',
    customSlot: 'name'
  },
  {
    title: '类型',
    key: 'type',
    customSlot: 'type'
  },
  {
    title: '路由地址',
    key: 'routePath'
  },
  {
    title: '权限标识',
    key: 'code'
  },
  {
    title: '排序',
    width: '120px',
    key: 'displayOrder'
  },
  {
    title: '状态',
    key: 'isEnable',
    customSlot: 'isEnable'
  },
  {
    title: '操作',
    key: 'option',
    customSlot: 'option'
  }
]

function toReset() {
  searchQuery.value = {
    name: '',
    code: '',
    routePath: ''
  }
}

onMounted(async () => {
  await toSearch()
  await loadMenuTree()
})

const loadMenuTree = async () => {
  let res = await getMenuTree();
  if (!res.hasError) {
    options.value = res;
  }
}

const toSearch = async () => {
  loading.value = true;
  let res = await getResourceTree(searchQuery.value);
  if (!res.hasError) {
    dataSource.value = res;
    loading.value = false;
  } else {
    loading.value = false;
  }
}

const resourceModel = ref({
  id: '0',
  name: '',
  displayOrder: 0,
  icon: '',
  routePath: '',
  code: '',
  isEnable: true,
  apiUrl: '',
  requestMethod: 1,
  releationIds: '',
  type: 1
})
const layFormRef = ref()
const isVisible = ref(false)

const options = ref([]);
const title = ref('新增')

// 是否显示弹窗
const changeVisible = (text: any, row: any) => {
  title.value = text
  if (row != null && text != '新建') {
    let info = JSON.parse(JSON.stringify(row))
    resourceModel.value = info
  } else {
    let releationIds = ''
    if (row){
      releationIds = row.type == 1 ? row.id : row.releationIds + '/' + row.id
    }
    resourceModel.value = {
      id: '0',
      name: '',
      displayOrder: 1000,
      icon: '',
      routePath: '',
      code: '',
      isEnable: true,
      apiUrl: '',
      requestMethod: 1,
      releationIds: releationIds,
      type: row == null ? 1 : row.type + 1
    }
  }
  isVisible.value = !isVisible.value
}

const toRemove = async (id: string)=> {
  let resourceId = id;
  layer.confirm('确认删除该条数据吗？', {
    title: '提示',
    btn: [
      {
        text: '确定',
        callback: async (id: any) => {
          let res = await delResource(resourceId);
          if (!res.hasError){
            layer.close(id)
            await toSearch()
          }
        }
      },
      {
        text: '取消',
        callback: (id: any) => {
          layer.close(id)
        }
      }
    ]
  })
}

const menuFields = ['name', 'type', 'displayOrder', 'routePath', 'isEnable']
const buttonFields = ['name', 'type', 'displayOrder', 'apiUrl', 'requestMethod', 'code', 'parentId', 'isEnable']

const toSubmit = () => {
  let fields = [];
  if (resourceModel.value.type != 3)
    fields = menuFields;
  else
    fields = buttonFields;
  layFormRef.value.validate(fields, async (isValidate: boolean) => {
    if (!isValidate)
      return false
    let res = resourceModel.value.id == '0' ? await addResource(resourceModel.value) : await editResource(resourceModel.value);
    if (!res.hasError) {
      layer.msg('保存成功！', { icon: 1, time: 1000 })
      isVisible.value = false
      await toSearch()
      // 刷新菜单树
      if (resourceModel.value.type != 3){
        await loadMenuTree()
      }
    }
  })
}

function toCancel() {
  isVisible.value = false
}
</script>

<style scoped>
.menu-box {
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

.table-style {
  margin-top: 10px;
}

.isChecked {
  display: inline-block;
  background-color: #e8f1ff;
  color: red;
}
</style>