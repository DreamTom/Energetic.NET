using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.Infrastructure;
using Serilog;

namespace Energetic.NET.ASPNETCore
{
    public static class ApplicationBuliderExtensions
    {
        /// <summary>
        /// 框架内置中间件启用
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseEnergeticNetDefault(this IApplicationBuilder app)
        {
            App.InitServiceProvider(app.ApplicationServices);
            //app.UseExceptionHandler();
            var envirment = App.GetRequiredService<IWebHostEnvironment>();
            var config = App.GetConfigOptions<SwaggerConfigOptions>();
            if (envirment.IsDevelopment() || (!envirment.IsDevelopment() && config.EnableInProduction))
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
            //app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors();//启用Cors
            app.UseRateLimiter();
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
