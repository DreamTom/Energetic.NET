using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.Resource.Dto
{
    public record ResourceAddRequest(string Name, int DisplayOrder)
    {
        public string? RoutePath { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string? Code { get; set; }

        public string ReleationIds { get; set; } = string.Empty;

        public string? Icon { get; set; }

        public ResourceType Type { get; set; }

        public bool IsEnable { get; set; }

        public string? ApiUrl { get; set; }

        public RequestMethod? RequestMethod { get; set; }
    }
}
