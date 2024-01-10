
namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public class UserResourceResponse
    {
        public long Id { get; set; }

        public List<UserResourceTreeResponse> Menus { get; set; } = [];

        public List<string?> Permissions { get; set; } = [];
    }
}
