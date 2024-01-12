import { defineStore } from 'pinia'
import { getLoginUserResources, menu, permission } from "../api/module/user";
import { treeToList } from '../library/treeUtil'

export const useUserStore = defineStore({
  id: 'user',
  state: () => {
    return {
      token: '',
      userInfo: {},
      permissions: [],
      menus: [],
      menuList: <any>[]
    }
  },
  actions: {
    async loadResources(){
      const res = await getLoginUserResources()
      if (!res.hasError){
        this.menus = res.menus;
        this.permissions = res.permissions;
        this.menuList = treeToList(res.menus)
      }
    },
    async loadMenus(){
      const { data, code } = await menu();
      if(code == 200) {
        this.menus = data;
      }
    },
    async loadPermissions(){
      const { data, code } = await permission();
      if(code == 200) {
        this.permissions = data;
      }
    }
  },
  persist: {
    storage: localStorage,
    paths: ['token', 'userInfo', 'permissions', 'menus', 'menuList' ],
  }
})