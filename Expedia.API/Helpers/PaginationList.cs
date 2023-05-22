using System;
using Microsoft.EntityFrameworkCore;

namespace Expedia.API.Helpers
{
	public class PaginationList<T>: List<T>
	{
		public PaginationList(int totalCount, int currentPage, int pageSize, List<T> items)
		{
            TotalCount = totalCount;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);

            CurrentPage = currentPage;
			PageSize = pageSize;
			AddRange(items);
        }

		public async static Task<PaginationList<T>> CreateAsync(
			int currentPage, int pageSize, IQueryable<T> result)
		{
            // get total count
            var totalCount = await result.CountAsync();
            // pagination
            var skip = (currentPage - 1) * pageSize;
            result = result.Skip(skip);
            result = result.Take(pageSize);
            // execute
            var items = await result.ToListAsync();
            // return
            return new PaginationList<T>(
                totalCount, currentPage, pageSize, items
                );
        }

        public int CurrentPage { get; set; }
		public int PageSize { get; set; }

		public int TotalPage { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;
    }
}

