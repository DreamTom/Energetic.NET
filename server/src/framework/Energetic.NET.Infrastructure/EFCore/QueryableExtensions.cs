using Energetic.NET.SharedKernel;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Expressions.Expression;

namespace Energetic.NET.Infrastructure.EFCore
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<TSource> OrderByPropName<TSource>(this IQueryable<TSource> source,
            string propName, string orderBy = "asc") where TSource : class
        {
            var propInfo = typeof(TSource).GetProperty(propName.ToPascal()) ?? throw new ArgumentException($"{nameof(propName)}在实体类{typeof(TSource).Name}中未找到！");
            var param = Parameter(typeof(TSource));
            var body = Property(param, propInfo);
            var convertedResult = Convert(body, typeof(object));
            var exp = Lambda<Func<TSource, object>>(convertedResult, param);
            return orderBy.ToLower() == "asc" ? source.OrderBy(exp) : source.OrderByDescending(exp);
        }

        public static async Task<PaginatedList<TDestination>> ToPageListAsync<TSource, TDestination>(this IQueryable<TSource> source,
            int pageNumber = 1, int pageSize = 20)
        {
            var count = await source.CountAsync();
            var pagedData = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var items = pagedData.Adapt<List<TDestination>>();
            return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
        }

        public static async Task<PaginatedList<TDestination>> ToPageListAsync<TSource, TDestination>(this IQueryable<TSource> source,
            IMapper mapper, int pageNumber = 1, int pageSize = 20, string? propName = default, string? orderBy = default) where TSource : class
        {
            var count = await source.CountAsync();
            if (!string.IsNullOrWhiteSpace(propName) && !string.IsNullOrWhiteSpace(orderBy))
                source = source.OrderByPropName(propName, orderBy);
            var query = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var items = await mapper.From(query).ProjectToType<TDestination>().ToListAsync();
            return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
        }
    }
}
