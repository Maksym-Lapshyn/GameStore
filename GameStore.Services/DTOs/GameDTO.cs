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

        public List<GenreDto> AllGenres { get; set; }

        public List<PlatformTypeDto> AllPlatforms { get; set; }

        public List<PublisherDto> AllPublishers { get; set; }

        public int SelectedPublisherId { get; set; }

        public List<int> SelectedGenreIds { get; set; }

        public List<int> SelectedPlatformIds { get; set; }
    }
}
