namespace Energetic.NET.Basic.Domain.Models
{
    public class FileAttachment : BaseCreatedEntity, IAggregateRoot
    {
        public FileAttachment(string name, string hashCode, string path, string contentType, long size)
        {
            Name = name;
            HashCode = hashCode;
            Path = path;
            ContentType = contentType;
            Size = size;
        }

        public string Name { get; init; }

        public string HashCode { get; init; }

        public string Path { get; init; }

        public string ContentType { get; init; }

        public long Size { get; init; }
    }
}
