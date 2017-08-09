using System;
using System.Collections.Generic;
using GameStore.Common.Entities;

namespace GameStore.Services.DTOs
{
	public class GameDto : BaseEntity
	{
		public GameDto()
		{
			GenresData = new List<GenreDto>();
			PlatformTypesData = new List<PlatformTypeDto>();
			PublisherData = new List<PublisherDto>();
			GenresInput = new List<string>();
			PlatformTypesInput = new List<string>();
		}

		public string Key { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsUpdated { get; set; }

		public decimal Price { get; set; }

		public DateTime DateAdded { get; set; }

		public DateTime DatePublished { get; set; }

		public int ViewsCount { get; set; }

		public short UnitsInStock { get; set; }

		public bool Discontinued { get; set; }

		public int CommentsCount { get; set; }

		public List<GenreDto> GenresData { get; set; }

		public List<PlatformTypeDto> PlatformTypesData { get; set; }

		public List<PublisherDto> PublisherData { get; set; }

		public string PublisherInput { get; set; }

		public List<string> GenresInput { get; set; }

		public List<string> PlatformTypesInput { get; set; }
	}
}
