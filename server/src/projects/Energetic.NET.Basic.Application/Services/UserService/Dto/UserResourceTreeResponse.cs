
namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public record UserResourceTreeResponse(long TempId, long ParentId, string Name)
    {
        public string? Id => RoutePath;

        public string? RoutePath {  get; set; }

        public string? Icon { get; set; }

        public string Title => Name;

        public List<UserResourceTreeResponse>? Children { get; set; }
    }
}
