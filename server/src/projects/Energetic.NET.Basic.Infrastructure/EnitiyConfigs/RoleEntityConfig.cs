namespace Energetic.NET.Basic.Infrastructure.EnitiyConfigs
{
    internal class RoleEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(e => e.Resources).WithMany(e => e.Roles).UsingEntity<RoleResource>();
        }
    }
}
