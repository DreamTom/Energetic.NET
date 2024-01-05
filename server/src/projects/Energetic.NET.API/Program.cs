using Energetic.NET.ASPNETCore.Extensions;
using Energetic.NET.Basic.Application.Email;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureEnergeticNetServices();
    builder.Services.AddConfigOptions<EmailNotifyConfigOptions>();
    var app = builder.Build();

    app.UseEnergeticNetDefault();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally { }
