using GameStore.Common.App_LocalResources;
using GameStore.Common.Entities;
using GameStore.Web.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GameStore.Web.Models
{
	public class GameViewModel : BaseEntity
	{
		public GameViewModel()
		{
			GenresInput = new List<string>();
			PlatformTypesInput = new List<string>();
			Comments = new List<CommentViewModel>();
		}

		[Required(ErrorMessageResourceName = "GameKeyIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "GameKey", ResourceType = typeof(GlobalResource))]
		public string Key { get; set; }

		[Required(ErrorMessageResourceName = "GameNameIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "GameName", ResourceType = typeof(GlobalResource))]
		public string Name { get; set; }

		[DataType("MultilineText")]
		[Required(ErrorMessageResourceName = "DescriptionIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "GameDescription", ResourceType = typeof(GlobalResource))]
		public string Description { get; set; }

		[Required(ErrorMessageResourceName = "PriceIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "Price", ResourceType = typeof(GlobalResource))]
		[Range(0.1, double.MaxValue, ErrorMessageResourceName = "PriceShouldBeGreaterThanZero", ErrorMessageResourceType = typeof(GlobalResource))]
		public decimal Price { get; set; }

		[Display(Name = "DateAdded", ResourceType = typeof(GlobalResource))]
		public DateTime DateAdded { get; set; }

		[Required(ErrorMessageResourceName = "DatePublishedIsRequired", ErrorMessageResourceType = typeof(GlobalResource))]
		[Display(Name = "DatePublished", ResourceType = typeof(GlobalResource))]
		public DateTime DatePublished { get; set; }

		public int ViewsCount { get; set; }

		public bool IsUpdated { get; set; }

		public int CommentsCount { get; set; }

		public ImageViewModel ImageData { get; set; }

        public HttpPostedFileBase ImageInput { get; set; }

		[Display(Name = "UnitsInStock", ResourceType = typeof(GlobalResource))]
		public short UnitsInStock { get; set; }

		[Display(Name = "Discontinued", ResourceType = typeof(GlobalResource))]
		public bool Discontinued { get; set; }

		public List<PublisherViewModel> PublisherData { get; set; }

		public List<GenreViewModel> GenresData { get; set; }

		public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

		[Display(Name = "Publisher", ResourceType = typeof(GlobalResource))]
		public string PublisherInput { get; set; }

		[Display(Name = "Genres", ResourceType = typeof(GlobalResource))]
		public List<string> GenresInput { get; set; }

		[Display(Name = "PlatformTypes", ResourceType = typeof(GlobalResource))]
		[CannotBeEmpty(ErrorMessageResourceName = "PlatformTypesInputCannotBeEmpty", ErrorMessageResourceType = typeof(GlobalResource))]
		public List<string> PlatformTypesInput { get; set; }

		public List<CommentViewModel> Comments { get; set; }
	}
}