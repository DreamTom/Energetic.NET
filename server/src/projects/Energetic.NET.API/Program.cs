using Energetic.NET.ASPNETCore.Extensions;
using Energetic.NET.Basic.Infrastructure;
using Energetic.NET.Common.Helpers;
using Energetic.NET.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureEnergeticNetServices();
var services = builder.Services;
services.RunModuleInitializers(ReflectionHelper.GetAllReferencedAssemblies());
var dbConnection = App.GetConfigOptions<DbConnectionConfigOptions>();
services.AddDbContext<BasicDbContext>(option =>
{
    option.UseMySql(dbConnection.ConnectionString, ServerVersion.AutoDetect(dbConnection.ConnectionString), builder =>
    {
    });
});

var app = builder.Build();

app.UseEnergeticNetDefault();

app.MapControllers();

app.Run();
