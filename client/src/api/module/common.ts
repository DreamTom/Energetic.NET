import Http from '../http';

//登录验证码
export const verificationImg = function () {
  return Http.get('/common/verificationImg')
}

//发送短信验证码
export const sendSmsVerificationCode = function (sendForm:any) {
  return Http.post('/common/sendSmsVerificationCode', sendForm)
}

//发送邮箱验证码
export const sendEmailVerificationCode = function (sendForm:any) {
  return Http.post('/common/sendEmailVerificationCode', sendForm)
}

//登录二维码
export const loginQrcode = function () {
  return Http.get('/login/loginQrcode')
}