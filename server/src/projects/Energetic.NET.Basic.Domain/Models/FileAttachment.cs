namespace Energetic.NET.Basic.Domain.Models
{
    public class FileAttachment(string name, string hashCode, string path, string contentType, long size)
        : BaseCreatedEntity, IEntity, IAggregateRoot
    {
        public string Name { get; init; } = name;

        public string HashCode { get; init; } = hashCode;

        public string Path { get; init; } = path;

        public string ContentType { get; init; } = contentType;

        public long Size { get; init; } = size;

        public long Id { get; private set; }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
