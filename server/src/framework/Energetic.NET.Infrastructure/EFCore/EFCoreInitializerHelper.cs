using Energetic.NET.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore
{
    public static class EFCoreInitializerHelper
    {
        /// <summary>
        /// 注入所有DbContext服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddAllDbContexts(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var dbConnection = App.GetConfigOptions<DbConnectionConfigOptions>();
            var builder = new Action<DbContextOptionsBuilder>(option =>
            {
                if (dbConnection.DbType == DbType.MySql)
                    option.UseMySql(dbConnection.ConnectionString, ServerVersion.AutoDetect(dbConnection.ConnectionString));
                else if (dbConnection.DbType == DbType.SqlServer)
                    option.UseSqlServer(dbConnection.ConnectionString);
                else if (dbConnection.DbType == DbType.Sqlite)
                    option.UseSqlite(dbConnection.ConnectionString);
                else
                    throw new NotSupportedException(dbConnection.DbType + "数据库暂不支持！");
                // 配置查询默认不跟踪
                //option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            Type[] types = [typeof(IServiceCollection), typeof(Action<DbContextOptionsBuilder>), typeof(ServiceLifetime), typeof(ServiceLifetime)];
            var methodAddDbContext = typeof(EntityFrameworkServiceCollectionExtensions)
                .GetMethod(nameof(EntityFrameworkServiceCollectionExtensions.AddDbContext), 1, types);
            foreach (var asm in assemblies)
            {
                Type[] typesInAsm = asm.GetTypes();
                //Register DbContext
                //GetTypes() include public/protected ones
                //GetExportedTypes only include public ones
                //so that XXDbContext in Agrregation can be internal to keep insulated
                foreach (var dbCtxType in typesInAsm
                    .Where(t => !t.IsAbstract && typeof(DbContext).IsAssignableFrom(t)))
                {
                    //similar to serviceCollection.AddDbContextPool<ECDictDbContext>(opt=>new DbContextOptionsBuilder(dbCtxOpt));
                    var methodGenericAddDbContext = methodAddDbContext?.MakeGenericMethod(dbCtxType);
                    methodGenericAddDbContext?.Invoke(null, new object[] { services, builder, ServiceLifetime.Scoped, ServiceLifetime.Scoped });
                }
            }
        }

    }
}
