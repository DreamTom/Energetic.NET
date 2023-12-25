namespace Energetic.NET.Infrastructure
{
    public record DbConnectionConfigOptions(string ConnectionString, string DbType, bool ToUnderline, int FieldDefaultLength)
    {
    }
}
