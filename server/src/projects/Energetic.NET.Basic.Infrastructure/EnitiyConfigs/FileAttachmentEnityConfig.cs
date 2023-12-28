namespace EnergeticCms.Infrastructure.EntityConfigs
{
    internal class FileAttachmentEnityConfig : IEntityTypeConfiguration<FileAttachment>
    {
        public void Configure(EntityTypeBuilder<FileAttachment> builder)
        {
            builder.Property(f => f.Path).HasMaxLength(512);
        }
    }
}
