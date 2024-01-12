using Energetic.NET.ASPNETCore.Extensions;
using Energetic.NET.Basic.Application.EmailService;
using Energetic.NET.Infrastructure.CommonServices;
using Energetic.NET.Infrastructure.ConfigOptions;
using Energetic.NET.Middleware.Authorization;
using Energetic.NET.Middleware.Logger;
using Energetic.NET.SharedKernel.ICommonServices;
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
    builder.Services.AddConfigOptions<LocalStorageConfigOptions>();
    builder.Services.AddConfigOptions<AppConfigOptions>();
    builder.Services.AddScoped<IStorageService, LocalStorageService>();
    builder.Services.Configure<MvcOptions>(option =>
    {
        option.Filters.Add<UserPermissionFilter>();
    });
    var app = builder.Build();

    app.UseEnergeticNetDefault();
    app.UseMiddleware<DbLoggingMiddleware>();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally { }
