using Microsoft.EntityFrameworkCore;

namespace Energetic.NET.Infrastructure
{
    public class PaginatedList<T>(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
    {
        public IReadOnlyList<T> Items { get; } = items;

        public int TotalCount { get; } = count;

        public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

        public int PageNumber { get; } = pageNumber;

        public int PageSize { get; } = pageSize;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
