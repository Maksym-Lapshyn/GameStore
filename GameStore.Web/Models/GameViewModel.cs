using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Web.Infrastructure.Attributes;

namespace GameStore.Web.Models
{
    public class GameViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public short UnitsInStock { get; set; }

        [ScaffoldColumn(false)]
        public bool Discontinued { get; set; }

        [DisplayName("Publisher")]
        public List<PublisherViewModel> AllPublishers { get; set; }

        [DisplayName("Genres")]
        public List<GenreViewModel> AllGenres { get; set; }

        [DisplayName("Platforms")]
        public List<PlatformTypeViewModel> AllPlatforms { get; set; }

        [DisplayName("Publisher")]
        [Required]
        [HiddenInput]
        [ScaffoldColumn(false)]
        public int SelectedPublisherId { get; set; }

        [DisplayName("Genres")]
        [CannotBeEmpty]
        public List<int> SelectedGenreIds { get; set; }

        [DisplayName("Platforms")]
        [CannotBeEmpty]
        public List<int> SelectedPlatformIds { get; set; }
    }
}