namespace Energetic.NET.Basic.Infrastructure.EntityConfigs
{
    internal class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.PhoneNumber).IsFixedLength().HasMaxLength(11);
            builder.Property("passwordHash").IsUnicode(true);
            builder.HasMany(e => e.Roles).WithMany(e => e.Users).UsingEntity<UserRole>();
        }
    }
}
