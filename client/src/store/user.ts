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
    }
  },
  persist: {
    storage: localStorage,
    paths: ['token', 'userInfo', 'permissions', 'menus', 'menuList' ],
  }
})