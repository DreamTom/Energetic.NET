import axios, { AxiosRequestHeaders, AxiosResponse, InternalAxiosRequestConfig } from 'axios';
import { useUserStore } from "../store/user";
import { layer } from '@layui/layui-vue';
import router from '../router';
import errCodeDic from './errCode';

type TAxiosOption = {
    timeout: number;
    baseURL: string;
}
 
const config: TAxiosOption = {
    timeout: 5000,
    baseURL: "http://localhost:5004/api"
}
 
class Http {
    service;
    config;
    constructor(config: TAxiosOption) {
        this.service = axios.create(config)
        this.config = config
        /* 请求拦截 */
        this.service.interceptors.request.use((config: InternalAxiosRequestConfig) => {
            const userInfoStore = useUserStore();
            if (userInfoStore.token) {
                //(config.headers as AxiosRequestHeaders).token = userInfoStore.token as string
                (config.headers as AxiosRequestHeaders).Authorization = 'Bearer ' + userInfoStore.token as string
            } else {
                if(router.currentRoute.value.path!=='/login') {
                    router.push('/login');
                }
            }
            return config
        }, error => {
            return Promise.reject(error);
        })

        /* 响应拦截 */
        this.service.interceptors.response.use((response: AxiosResponse<any>) => {
            return response.data;
        }, error => {
            let errResponse = error.response;
            let message = error.message;
            if(errResponse && errResponse.status){
                message = errResponse.data.errorCode == 0 ? errResponse.data.message : errCodeDic[errResponse.data.errorCode];
                switch (errResponse.status) {
                    case 401:
                        layer.msg(
                            message, 
                            { icon : 3}, function(){
                                router.push('/login');
                                layer.closeAll()
                            });
                        return;
                    case 503:
                    case 429:
                        message = "服务暂时不可用，请稍后再试"
                        break;        
                    default:
                        break;
                }
            }
            layer.msg(message,{icon : 2})
            return { hasError: true, message: message };
        })
    }

    /* GET 方法 */
    get<T>(url: string, params?: object, _object = {}): Promise<any> {
        return this.service.get(url, { params, ..._object })
    }
    /* POST 方法 */
    post<T>(url: string, params?: object, _object = {}): Promise<any> {
        return this.service.post(url, params, _object)
    }
    /* PUT 方法 */
    put<T>(url: string, params?: object, _object = {}): Promise<any> {
        return this.service.put(url, params, _object)
    }
    /* DELETE 方法 */
    delete<T>(url: string, params?: any, _object = {}): Promise<any> {
        return this.service.delete(url, { params, ..._object })
    }
}

export default new Http(config)