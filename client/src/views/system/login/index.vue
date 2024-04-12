<template>
  <lay-container fluid="true" class="login-box">
    <lay-card>
      <lay-form style="margin-top: 10px">
        <lay-row>
          <lay-col :md="5">
            <lay-form-item label="登录账号" label-width="80">
              <lay-input
                v-model="searchQuery.loginAccount"
                placeholder="请输入"
                size="sm"
                :allow-clear="true"
                style="width: 98%"
              ></lay-input>
            </lay-form-item>
          </lay-col>
          <lay-col :md="6">
            <lay-form-item label="登录时间" label-width="80">
              <lay-date-picker
                size="sm"
                v-model="searchQuery.rangeTime"
                range
                type="datetime"
                :placeholder="['开始日期', '结束日期']"
              ></lay-date-picker
            ></lay-form-item>
          </lay-col>
          <lay-col :md="5">
            <lay-form-item label-width="20">
              <lay-button v-permission="['sys:users:loginHistoryQuery']"
                style="margin-left: 20px"
                type="normal"
                size="sm"
                @click="loadDataSource"
              >
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
        <template #loginResult="{ row }">
          <div v-if="row.loginResult == 1">
            <lay-tag color="#2dc570" variant="light">登录成功</lay-tag>
          </div>
          <div v-else>
            <lay-tag color="#F5319D" variant="light">登录失败</lay-tag>
          </div>
        </template>
        <template #message="{ row }">
          <lay-tooltip :visible="false" trigger="hover" :content="row.message">
            <div class="oneRow">{{ row.message }}</div>
          </lay-tooltip>
        </template>
        <template v-slot:toolbar> </template>
        <template v-slot:operator="{ row }">
          <lay-popconfirm
            content="确定要删除此条登录记录吗?"
            @confirm="confirm(row)"
            @cancel="cancel"
          >
            <lay-button size="xs" border="red" border-style="dashed"
              >删除</lay-button
            >
          </lay-popconfirm>
        </template>
      </lay-table>
    </div>
  </lay-container>
</template>
<script setup lang="ts">
import { ref, reactive,onMounted } from 'vue'
import { layer } from '@layui/layui-vue'
import { getUserLoginHistories } from '../../../api/module/user';

const searchQuery = reactive({
  loginAccount: '',
  rangeTime: [],
  pageNumber: 1,
  pageSize: 20,
  orderBy: '',
  propName: ''
})

onMounted(async () => {
  await loadDataSource()
})

const toReset = async()=> {
  searchQuery.loginAccount = '';
  searchQuery.rangeTime = [];
  await loadDataSource();
}

const loading = ref(false)
const page = reactive({ current: 1, limit: 10, total: 100 })
const columns = ref([
  { title: '登录账号', key: 'loginAccount', sort: 'desc' },
  {
    title: '登录方式',
    width: '160px',
    key: 'loginWayDesc'
  },
  { title: 'IP地址', key: 'loginIp'},
  { title: '登录地点', key: 'loginLocation'},
  { title: '操作系统', key: 'operatingSystem' },
  { title: '浏览器', key: 'browser' },
  {
    title: '登录结果',
    width: '120px',
    key: 'loginResult',
    customSlot: 'loginResult'
  },
  { title: '登录时间', width: '220px', key: 'createdTime', sort: 'desc'},
  { title: '备注', width: '220px', key: 'message', customSlot: 'message' }
])

const sortChange = async (key: any, sort: string) => {
  searchQuery.orderBy = sort
  searchQuery.propName = key
  await loadDataSource()
}

const dataSource = ref([])

const loadDataSource = async() => {
  loading.value = true;
  searchQuery.pageNumber = page.current;
  searchQuery.pageSize = page.limit;
  let res = await getUserLoginHistories(searchQuery);
  if (!res.hasError) {
    dataSource.value = res.items;
    page.total = res.totalCount;
    loading.value = false;
  } else {
    loading.value = false;
  }
}
function confirm(row: any) {
  layer.msg('您已成功删除')
}
function cancel() {
  layer.msg('您已取消操作')
}
</script>

<style scoped>
.login-box {
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
.oneRow {
  width: 220px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>