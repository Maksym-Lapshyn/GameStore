namespace GameStore.Services.DTOs
{
	public class PaginatorDto
	{
		public int TotalItems { get; set; }

		public int CurrentPage { get; set; }

		public int PageSize { get; set; }

		public int TotalPages { get; set; }
	}
}