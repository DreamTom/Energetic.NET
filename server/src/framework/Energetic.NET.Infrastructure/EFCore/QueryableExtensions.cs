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
            return orderBy.Equals("asc", StringComparison.CurrentCultureIgnoreCase) ? source.OrderBy(exp) : source.OrderByDescending(exp);
        }

        public static async Task<PaginatedList<TDestination>> ToPageListAsync<TSource, TDestination>(this IQueryable<TSource> source,
            PaginatedQueryRequest paginatedQuery)
        {
            var count = await source.CountAsync();
            var pagedData = await source.Skip((paginatedQuery.PageNumber - 1) * paginatedQuery.PageSize).Take(paginatedQuery.PageSize).ToListAsync();
            var items = pagedData.Adapt<List<TDestination>>();
            return new PaginatedList<TDestination>(items, count, paginatedQuery.PageNumber, paginatedQuery.PageSize);
        }

        public static async Task<PaginatedList<TDestination>> ToPageListAsync<TSource, TDestination>(this IQueryable<TSource> source,
            IMapper mapper, PaginatedQueryRequest paginatedQuery) where TSource : class
        {
            var count = await source.CountAsync();
            if (!string.IsNullOrWhiteSpace(paginatedQuery.PropName) && !string.IsNullOrWhiteSpace(paginatedQuery.OrderBy))
                source = source.OrderByPropName(paginatedQuery.PropName, paginatedQuery.OrderBy);
            source = source.Skip((paginatedQuery.PageNumber - 1) * paginatedQuery.PageSize).Take(paginatedQuery.PageSize);
            var items = await mapper.From(source).ProjectToType<TDestination>().ToListAsync();
            return new PaginatedList<TDestination>(items, count, paginatedQuery.PageNumber, paginatedQuery.PageSize);
        }
    }
}
