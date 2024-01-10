using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.UserService.Dto
{
    public record UserQueryRequest(int PageNumber, int PageSize) : PaginatedQueryRequest(PageNumber, PageSize)
    {
        public string? UserName { get; set; }

        public string? NickName { get; set; }

        public Gender? Gender { get; set; }
    }
}
