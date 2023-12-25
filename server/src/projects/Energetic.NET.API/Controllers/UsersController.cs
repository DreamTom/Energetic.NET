using Energetic.NET.Basic.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Energetic.NET.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/users")]
    public class UsersController : BaseController
    {
        [AllowAnonymous]
        [HttpGet()]
        public ActionResult Test()
        {
            var user = new User("aaaa");
            return Ok();
        }
    }
}
