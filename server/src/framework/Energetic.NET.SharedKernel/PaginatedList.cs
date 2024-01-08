namespace Energetic.NET.SharedKernel
{
    public class PaginatedList<T>(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
    {
        public IReadOnlyList<T> Items { get; } = items;

        public int TotalCount { get; } = count;

        public int TotalPages { get; } = (int)Math.Ceiling(count / (double)pageSize);

        public int PageNumber { get; } = pageNumber;

        public int PageSize { get; } = pageSize;
    }
}
