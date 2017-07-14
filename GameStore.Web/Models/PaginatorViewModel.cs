using System;

namespace GameStore.Web.Models
{
	public class PaginatorViewModel
	{
		public int TotalItems { get; set; }

		public int CurrentPage { get; set; }

		public int PageSize { get; set; }

		public int TotalPages { get; set; }

		public PaginatorViewModel Initialize(int totalItems, int page, int pageSize)
		{
			int totalPages;

			if (pageSize == 0)
			{
				totalPages = 1;
			}
			else
			{
				totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
			}

			CurrentPage = page;
			TotalItems = totalItems;
			PageSize = pageSize;
			TotalPages = totalPages;

			return this;
		}
	}
}