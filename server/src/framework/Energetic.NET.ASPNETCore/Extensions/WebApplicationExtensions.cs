using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.Infrastructure;
using Serilog;

namespace Energetic.NET.ASPNETCore
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// 框架内置中间件启用
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseEnergeticNetDefault(this WebApplication app)
        {
            App.InitServiceProvider(app.Services);
            //app.UseExceptionHandler();
            var config = App.GetConfigOptions<SwaggerConfigOptions>();
            if (app.Environment.IsDevelopment() || (!app.Environment.IsDevelopment() && config.EnableInProduction))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"/swagger/{config.Version}/swagger.json", config.Title);
                    options.RoutePrefix = string.Empty;
                });
            }
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors();//启用Cors
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}
