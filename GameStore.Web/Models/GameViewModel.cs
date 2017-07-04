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

        [DisplayName("Units in stock")]
        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        [DisplayName("Publisher")]
        public List<PublisherViewModel> PublishersData { get; set; }

        [DisplayName("Genres")]
        public List<GenreViewModel> GenresData { get; set; }

        [DisplayName("Platforms")]
        public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

        [DisplayName("Publisher")]
        [Required]
        [HiddenInput]
        public int PublisherInput { get; set; }

        [DisplayName("Genres")]
        [CannotBeEmpty]
        public List<int> GenresInput { get; set; }

        [DisplayName("Platforms")]
        [CannotBeEmpty]
        public List<int> PlatformTypesInput { get; set; }
    }
}