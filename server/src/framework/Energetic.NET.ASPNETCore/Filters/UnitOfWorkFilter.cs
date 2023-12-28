using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Transactions;

namespace Energetic.NET.ASPNETCore.Filters
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var unitOfAttr = GetUnitOfWorkAttribute(context.ActionDescriptor);
            if (unitOfAttr == null)
            {
                await next();
                return;
            }
            using TransactionScope txScope = new(TransactionScopeAsyncFlowOption.Enabled);
            List<DbContext> dbCtxs = [];
            foreach (var dbCtxType in unitOfAttr.DbContextTypes)
            {
                //用HttpContext的RequestServices
                //确保获取的是和请求相关的Scope实例
                var sp = context.HttpContext.RequestServices;
                DbContext dbCtx = (DbContext)sp.GetRequiredService(dbCtxType);
                dbCtxs.Add(dbCtx);
            }
            var result = await next();
            if (result.Exception == null)
            {
                foreach (var dbCtx in dbCtxs)
                {
                    await dbCtx.SaveChangesAsync();
                }
                txScope.Complete();
            }
        }

        private static UnitOfWorkAttribute? GetUnitOfWorkAttribute(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is not ControllerActionDescriptor caDesc)
            {
                return null;
            }

            var unitOfAttr = caDesc.ControllerTypeInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            if (unitOfAttr != null)
            {
                return unitOfAttr;
            }
            else
            {
                return caDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            }

        }
    }
}
