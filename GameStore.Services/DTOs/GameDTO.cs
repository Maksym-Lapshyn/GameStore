using System.Collections.Generic;

namespace GameStore.Services.DTOs
{
	public class GameDto
	{
		public int Id { get; set; }

		public string Key { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Price { get; set; }

		public short UnitsInStock { get; set; }

		public bool Discontinued { get; set; }

		public IEnumerable<GenreDto> GenresData { get; set; }

		public IEnumerable<PlatformTypeDto> PlatformTypesData { get; set; }

		public IEnumerable<PublisherDto> PublishersData { get; set; }

		public int PublisherInput { get; set; }

		public IEnumerable<int> GenresInput { get; set; }

		public IEnumerable<int> PlatformTypesInput { get; set; }

		public GameDto()
		{
			GenresData = new List<GenreDto>();
			PlatformTypesData = new List<PlatformTypeDto>();
			PublishersData = new List<PublisherDto>();
			GenresInput = new List<int>();
			PlatformTypesInput = new List<int>();
		}
	}
}
