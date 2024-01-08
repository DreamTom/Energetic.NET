namespace Energetic.NET.Basic.Domain.Models
{
    public class Role(string name, string code, string? description) : BaseAuditableEntity, IAggregateRoot
    {
        public string Name { get; private set; } = name;

        public string Code { get; private set; } = code;

        public string? Description { get; private set; } = description;

        public List<User> Users { get; } = [];

        public List<Resource> Resources { get; private set; } = [];

        public void Update(string name, string code, string? description)
        {
            Name = name;
            Code = code;
            Description = description;
        }

        public void SetResources(List<Resource> resources)
        {
            Resources = resources;
        }
    }
}
