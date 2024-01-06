namespace Energetic.NET.Basic.Infrastructure.EnitiyConfigs
{
    internal class ResourceEntityConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.Property(f => f.RequestMethod)
                .HasConversion<string>()
                .HasMaxLength(10);
        }
    }
}
