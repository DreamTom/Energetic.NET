using Energetic.NET.ASPNETCore.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureEnergeticNetServices();

var app = builder.Build();

app.UseEnergeticNetDefault();

app.Run();
