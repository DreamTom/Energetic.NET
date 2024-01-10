namespace Energetic.NET.Basic.Infrastructure.EntityConfigs
{
    internal class FileAttachmentEnityConfig : IEntityTypeConfiguration<FileAttachment>
    {
        public void Configure(EntityTypeBuilder<FileAttachment> builder)
        {
            builder.Property(f => f.Path).HasMaxLength(512);
            builder.Property(f => f.HashCode).HasMaxLength(256);
            builder.Property(f => f.Name).HasMaxLength(128);
        }
    }
}
