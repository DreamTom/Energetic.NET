namespace Energetic.NET.Basic.Domain.Models
{
    public class Role : BaseAuditableEntity, IAggregateRoot
    {
        public Role(string name, string? desc)
        {
            Name = name;
            Desc = desc;
        }

        public string Name { get; private set; }

        public string? Desc { get; private set; }

        public List<User> Users { get; } = [];

        public List<Resource> Resources { get; } = [];

        public void Update(string name, string? desc)
        {
            Name = name;
            Desc = desc;
        }
    }
}
