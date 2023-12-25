using Energetic.NET.ASPNETCore.ConfigOptions;
using Energetic.NET.Infrastructure;

namespace Energetic.NET.ASPNETCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEnergeticNetDefault(this IApplicationBuilder app)
        {
            App.InitServiceProvider(app.ApplicationServices);
            var envirment =  App.GetRequiredService<IWebHostEnvironment>();
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
            //app.UseEventBus();
            app.UseCors();//启用Cors
            app.UseForwardedHeaders();
            //app.UseHttpsRedirection();//不能与ForwardedHeaders很好的工作，而且webapi项目也没必要配置这个
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
