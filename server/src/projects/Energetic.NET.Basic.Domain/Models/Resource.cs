namespace Energetic.NET.Basic.Domain.Models
{
    public class Resource : BaseAuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public long? ParentId { get; private set; }

        public string? Icon { get; private set; }

        public int DisplayOrder { get; private set; }

        public bool IsFolder { get; private set; }

        public bool IsMenu { get; private set; }

        public bool IsHide { get; private set; }

        public string Route { get; private set; } = string.Empty;

        public string? HttpMethod { get; private set; }

        public List<Role> Roles { get; } = [];

        public Resource(string name, string code, long? parentId)
        {
            Name = name;
            Code = code;
            ParentId = parentId;
        }

        public void UpdateMenu(string? icon, int displayOrder, bool isFolder, bool isHide, string route)
        {
            Icon = icon;
            DisplayOrder = displayOrder;
            IsFolder = isFolder;
            IsHide = isHide;
            Route = route;
        }

        public void UpdateButton(bool isHide, string route, string httpMethod)
        {
            IsHide = isHide;
            Route = route;
            HttpMethod = httpMethod;
        }
    }
}
