﻿using static System.Linq.Expressions.Expression;

namespace Energetic.NET.Infrastructure.EFCore
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<TSource> OrderByPropName<TSource>(this IQueryable<TSource> source,
            string propName, bool isDesc = false) where TSource : class
        {
            var propInfo = typeof(TSource).GetProperty(propName) ?? throw new ArgumentException($"{nameof(propName)}在实体类{typeof(TSource).Name}中未找到！");
            var param = Parameter(typeof(TSource));
            var body = Property(param, propInfo);
            var convertedResult = Convert(body, typeof(object));
            var exp = Lambda<Func<TSource, object>>(convertedResult, param);
            return isDesc ? source.OrderBy(exp) : source.OrderByDescending(exp);
        }
    }
}
