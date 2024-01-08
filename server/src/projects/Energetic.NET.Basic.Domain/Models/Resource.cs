using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Domain.Models
{
    public class Resource(string name, int displayOrder) : BaseAuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = name;

        /// <summary>
        /// 权限代码
        /// </summary>
        public string? Code { get; private set; }

        public long ParentId { get; private set; }

        public string ReleationIds { get; private set; } = string.Empty;

        /// <summary>
        /// 路由地址
        /// </summary>
        public string? RoutePath { get; private set; }

        public string? Icon { get; private set; }

        public int DisplayOrder { get; private set; } = displayOrder;

        public ResourceType Type { get; private set; }

        public bool IsEnable { get; private set; }

        public string? ApiUrl { get; private set; }

        public RequestMethod? RequestMethod { get; private set; }

        public List<Role> Roles { get; } = [];
    }
}
