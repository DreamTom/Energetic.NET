using Energetic.NET.Basic.Application.ResourceService.Dto;

namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public class LoginResponse
    {
        public required string Token { get; set; }

        public required UserResponse UserInfo { get; set; }
    }
}
