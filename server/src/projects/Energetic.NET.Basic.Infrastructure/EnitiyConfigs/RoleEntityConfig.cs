namespace Energetic.NET.Basic.Infrastructure.EnitiyConfigs
{
    internal class RoleEntityConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Description).HasMaxLength(256);
            builder.HasMany(r => r.Resources).WithMany(r => r.Roles).UsingEntity<RoleResource>();
        }
    }
}
