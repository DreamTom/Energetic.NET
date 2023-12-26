using Energetic.NET.ASPNETCore.Extensions;
using Energetic.NET.Basic.Infrastructure;
using Energetic.NET.Common.Helpers;
using Energetic.NET.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureEnergeticNetServices();
//var services = builder.Services;
//services.RunModuleInitializers(ReflectionHelper.GetAllReferencedAssemblies());

var app = builder.Build();

app.UseEnergeticNetDefault();

app.Run();
