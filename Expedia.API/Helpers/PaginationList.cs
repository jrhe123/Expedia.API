using System;
using Microsoft.EntityFrameworkCore;

namespace Expedia.API.Helpers
{
	public class PaginationList<T>: List<T>
	{
		public PaginationList(int currentPage, int pageSize, List<T> items)
		{
			CurrentPage = currentPage;
			PageSize = pageSize;
			AddRange(items);
        }

		public async static Task<PaginationList<T>> CreateAsync(
			int currentPage, int pageSize, IQueryable<T> result)
		{
            // pagination
            var skip = (currentPage - 1) * pageSize;
            result = result.Skip(skip);
            result = result.Take(pageSize);

            var items = await result.ToListAsync();

            return new PaginationList<T>(currentPage, pageSize, items);
        }

        public int CurrentPage { get; set; }
		public int PageSize { get; set; }
	}
}

