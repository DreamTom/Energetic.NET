import { defineStore } from 'pinia'
import { getLoginUserResources, menu, permission } from "../api/module/user";

export const useUserStore = defineStore({
  id: 'user',
  state: () => {
    return {
      token: '',
      userInfo: {},
      permissions: [],
      menus: [],
    }
  },
  actions: {
    async loadResources(){
      const res = await getLoginUserResources()
      if (!res.hasError){
        this.menus = res.menus;
        this.permissions = res.permissions;
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
    paths: ['token', 'userInfo', 'permissions', 'menus' ],
  }
})