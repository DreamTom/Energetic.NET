namespace Energetic.NET.ASPNETCore.ConfigOptions
{
    public record SwaggerConfigOptions(string Title, string Description, string Version,
        string XmlFileName, bool EnableInProduction, Contact Contact);

    public record Contact(string Name, string Email, string Url);
}
