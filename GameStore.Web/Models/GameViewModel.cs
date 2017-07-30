using System;
using GameStore.Web.Infrastructure.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GameStore.Web.Models
{
	public class GameViewModel
	{
		[HiddenInput]
		public int Id { get; set; }

		[Required]
		public string Key { get; set; }

		[Required]
		public string Name { get; set; }

		[DataType("MultilineText")]
		[Required]
		public string Description { get; set; }

		[Required]
		[Range(0.1, double.MaxValue)]
		public decimal Price { get; set; }

		public DateTime DateAdded { get; set; }

		[DisplayName("Date published")]
		[Required]
		public DateTime DatePublished { get; set; }

		public int ViewsCount { get; set; }

        public bool IsUpdated { get; set; }

        public int CommentsCount { get; set; }

		[DisplayName("Units in stock")]
		public short UnitsInStock { get; set; }

		public bool Discontinued { get; set; }

		[DisplayName("Publishers")]
		public List<PublisherViewModel> PublishersData { get; set; }

		[DisplayName("Genres")]
		public List<GenreViewModel> GenresData { get; set; }

		[DisplayName("PlatformTypes")]
		public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

		[DisplayName("Publishers")]
		[Required]
		[HiddenInput]
		public string PublisherInput { get; set; }

		[DisplayName("Genres")]
		[CannotBeEmpty]
		public List<string> GenresInput { get; set; }

		[DisplayName("PlatformTypes")]
		[CannotBeEmpty]
		public List<string> PlatformTypesInput { get; set; }
	}
}