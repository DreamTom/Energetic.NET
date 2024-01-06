using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Domain.Models
{
    public class Resource(string name, string routePath, int displayOrder) : BaseAuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = name;

        /// <summary>
        /// 权限代码
        /// </summary>
        public string? Code { get; private set; }

        public long ParentId { get; private set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RoutePath { get; private set; } = routePath;

        public string? Icon { get; private set; }

        public int DisplayOrder { get; private set; } = displayOrder;

        public bool IsFolder { get; private set; }

        public bool IsMenu { get; private set; }

        public bool IsHide { get; private set; }

        public string? ApiUrl { get; private set; }

        public RequestMethod? RequestMethod { get; private set; }

        public List<Role> Roles { get; } = [];

        public void AddFolder(bool isHide, long parentId)
        {
            IsFolder = true;
            ParentId = parentId;
            IsHide = isHide;
        }

        public void AddMenu(bool isHide, string? icon, long parentId)
        {
            IsMenu = true;
            Icon = icon;
            ParentId = parentId;
            IsHide = isHide;
        }

        public void AddButton(bool isHide, long parentId, string code, string apiUrl, RequestMethod requestMethod)
        {
            IsHide = isHide;
            ParentId = parentId;
            ApiUrl = apiUrl;
            RequestMethod = requestMethod;
            Code = code;
        }
    }
}
