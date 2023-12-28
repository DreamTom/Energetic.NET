using Energetic.NET.Basic.Application.User.Dto;
using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.Models;
using Energetic.NET.Basic.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [UnitOfWork(typeof(BasicDbContext))]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private readonly BasicDbContext _basicDbContext;

        public UsersController(BasicDbContext basicDbContext)
        {
            _basicDbContext = basicDbContext;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult Register(RegRequest regRequest)
        {
            if (regRequest.RegisterWay == RegisterWay.UserName)
            {
                var user = new User(regRequest.RegisterWay, regRequest.NickName);
                user.RealName = user.NickName;
                user.EmailAddress = regRequest.EmailAddress;
                user.PhoneNumber = regRequest.PhoneNumber;
                user.Gender = regRequest.Gender;
                user.ChangePassword(regRequest.Password);
                _basicDbContext.Users.Add(user);
            }
            return Ok();
        }
    }
}
