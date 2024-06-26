﻿using Energetic.NET.Basic.Application.UserService.Dto;
using Energetic.NET.Basic.Application.UserService;
using Energetic.NET.Basic.Domain.Constants;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Basic.Domain.Services;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authorization;
using Energetic.NET.Basic.Application.Services.UserService.Dto;

namespace Energetic.NET.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Module("用户管理")]
    [UnitOfWork(typeof(BasicDbContext))]
    [Route("api/users")]
    public class UsersController(IUserDomainRepository userDomainRepository,
        IRoleDomainRepository roleDomainRepository,
        UserDomainService userDomainService,
        IUserAppService userAppService,
        IMapper mapper) : BaseController
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="captcha"></param>
        /// <param name="regRequest"></param>
        /// <returns></returns>
        [Function("用户注册")]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromServices] ICaptcha captcha, RegRequest regRequest)
        {
            if (!captcha.Validate(regRequest.CaptchaId, regRequest.VerificationCode))
                return ValidateFailed("验证码错误");
            if (await userDomainRepository.IsExistsUserNameAsync(regRequest.UserName))
                return ValidateFailed("用户名已存在");
            var addUser = new User(RegisterWay.UserName, regRequest.NickName);
            addUser.AddByUserName(regRequest.UserName, regRequest.RealName, regRequest.Password, regRequest.Gender);
            _ = await userDomainRepository.AddAsync(addUser);
            return Ok(addUser.Id);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="captcha"></param>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [Function("用户登录")]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromServices] ICaptcha captcha, LoginRequest loginRequest)
        {
            User user = null;
            string token = string.Empty;
            if (loginRequest.LoginWay == LoginWay.UserName)
            {
                if (!captcha.Validate(loginRequest.CaptchaId, loginRequest.VerificationCode))
                    return ValidateFailed("验证码错误");
                var (LoginFailedResult, Token, User) = await userDomainService.LoginByUserNameAndPasswordAsync(loginRequest.UserName, loginRequest.Password);
                if (LoginFailedResult != null)
                    return ValidateFailed(LoginFailedResult);
                user = User;
                token = Token;
            }
            else if (loginRequest.LoginWay == LoginWay.PhoneNumber)
            {
                var (LoginFailedResult, Token, User) = await userDomainService.LoginByPhoneNumberAsync(loginRequest.PhoneNumber, loginRequest.SecondCode);
                if (LoginFailedResult != null)
                    return ValidateFailed(LoginFailedResult);
                user = User;
                token = Token;
            }
            else if (loginRequest.LoginWay == LoginWay.EmailAddress)
            {
                var (LoginFailedResult, Token, User) = await userDomainService.LoginByEmailAddressAsync(loginRequest.EmailAddress, loginRequest.SecondCode);
                if (LoginFailedResult != null)
                    return ValidateFailed(LoginFailedResult);
                user = User;
                token = Token;
            }
            return Ok(new LoginResponse
            {
                Token = token,
                UserInfo = mapper.Map<UserResponse>(user)
            });
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userAdd"></param>
        /// <returns></returns>
        [Function("新增用户")]
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Add([FromServices] IConfiguration configuration, UserEditRequest userAdd)
        {
            if (await userDomainRepository.IsExistsUserNameAsync(userAdd.UserName))
                return ValidateFailed("用户名已存在");
            var defaultPassword = configuration[ConfigKey.DefaultPassword];
            if (string.IsNullOrWhiteSpace(defaultPassword))
                defaultPassword = "123456";
            var user = new User(RegisterWay.UserName, userAdd.NickName);
            var roles = await roleDomainRepository.FindByIdsAsync(userAdd.RoleIds);
            user.SetRoles(roles);
            user.AddOrUpdate(userAdd.UserName, userAdd.RealName, userAdd.Gender, userAdd.AvatarId, userAdd.IsEnable);
            user.SetPassword(defaultPassword);
            await userDomainRepository.AddAsync(user);
            return Ok(mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="id"></param>
        /// <param name="userAdd"></param>
        /// <returns></returns>
        [Function("修改用户")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<UserResponse>> Edit([FromServices] IConfiguration configuration, long id, UserEditRequest userAdd)
        {
            if (!await userDomainRepository.IsExistsIdAsync(id))
                return DataNotFound("用户不存在或已被删除");
            if (await userDomainRepository.IsExistsUserNameAsync(userAdd.UserName, id))
                return ValidateFailed("用户名已存在");

            var user = await userDomainService.UpdateUserRolesAsync(id, userAdd.RoleIds);
            user.AddOrUpdate(userAdd.UserName, userAdd.RealName, userAdd.Gender, userAdd.AvatarId, userAdd.IsEnable);
            user.ChangeNickName(userAdd.NickName);
            userDomainRepository.Update(user);
            return Ok(mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Function("删除用户")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var user = await userDomainRepository.GetUserIncludeRolesAsync(id);
            if (user == null)
                return DataNotFound("用户不存在或已被删除");
            user.SetRoles(null);
            user.LogicDelete();
            return Ok(id);
        }

        /// <summary>
        /// 当前已登录用户权限
        /// </summary>
        /// <returns></returns>
        [Function("获取已登录用户权限")]
        [NoPermissionCheck]
        [HttpGet("loginUserResources")]
        public async Task<ActionResult<UserResourceResponse>> GetLoginUserResources()
        {
            return Ok(await userAppService.GetUserResourcesAsync(CurrentUser.Id));
        }

        /// <summary>
        /// 查看用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Function("查看用户")]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<UserResponse>> Detail(long id)
        {
            var user = await userDomainRepository.FindByIdAsync(id);
            if (user == null)
                return DataNotFound("用户不存在或已被删除");
            return Ok(mapper.Map<UserResponse>(user));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        [Function("用户列表")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserResponse>>> GetPageList([FromQuery] UserQueryRequest userQuery)
        {
            return Ok(await userAppService.GetPageListAsync(userQuery));
        }

        /// <summary>
        /// 用户登日志录列表
        /// </summary>
        /// <param name="userLoginHistoryQueryRequest"></param>
        /// <returns></returns>
        [Function("用户登录日志列表")]
        [HttpGet("loginHistories")]
        public async Task<ActionResult<PaginatedList<UserLoginHistoryResponse>>> GetPageUserLoginHistories
            ([FromQuery] UserLoginHistoryQueryRequest userLoginHistoryQueryRequest)
        {
            return Ok(await userAppService.GetUserLoginHistoriesAsync(userLoginHistoryQueryRequest));
        }
    }
}
