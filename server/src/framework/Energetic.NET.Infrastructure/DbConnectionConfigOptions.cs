namespace Energetic.NET.Infrastructure
{
    public class DbConnectionConfigOptions
    {
        public string ConnectionString { get; set; } = null!;

        public string DbType { get; set; } = null!;

        public bool ToUnderline { get; set; }

        public int FieldDefaultLength { get; set; }

        public bool EnableSoftDeletionFilter { get; set; }

        public bool EnableUsingSnowflakeId { get; set; }
    }
}
