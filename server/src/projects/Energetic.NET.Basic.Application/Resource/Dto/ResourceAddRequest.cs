using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.Resource.Dto
{
    public record ResourceAddRequest(string Name, string RoutePath, int DisplayOrder)
    {
        /// <summary>
        /// 权限代码
        /// </summary>
        public string? Code { get; set; }

        public long ParentId { get; set; }

        public string? Icon { get; set; }

        public bool IsFolder { get; set; }

        public bool IsMenu { get; set; }

        public bool IsHide { get; set; }

        public string? ApiUrl { get; set; }

        public RequestMethod? RequestMethod { get; set; }
    }
}
