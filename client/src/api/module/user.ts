import Http from '../http';

export const login = function(loginForm: any) {
    return Http.post('/user/login', loginForm)
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