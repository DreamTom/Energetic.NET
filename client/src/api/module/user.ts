import http from '../http';
import Http from '../http';

export const login = function(loginForm: any) {
    return Http.post('/users/login', loginForm)
}

export const register = function(registerForm: any) {
    return Http.post('/users/register', registerForm);
}

export const menu = function() {
    return Http.get('/user/menu') 
}

export const permission = function() {
    return Http.get('/user/permission') 
}

export const getUsers = function(userQuery: any) {
    return Http.get('/users', userQuery)
}

export const addUser = function(addForm: any){
    return Http.post('/users', addForm)
}

export const editUser = function(editForm: any){
    return Http.put(`/users/${editForm.id}`, editForm)
}

export const delUser = function(id: string){
    return Http.delete('/users/'+ id);
}

export const getLoginUserResources = function(){
    return Http.get('users/loginUserResources')
}