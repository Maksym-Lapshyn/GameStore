using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short UnitsInStock { get; set; }

        [Required]
        public bool Discontinued { get; set; }

        public int? PublisherId { get; set; }

        public PublisherViewModel Publisher { get; set; }


        public IEnumerable<CommentViewModel> Comments { get; set; }

        public IEnumerable<GenreViewModel> Genres { get; set; }

        public IEnumerable<PlatformTypeViewModel> PlatformTypes { get; set; }

        public GameViewModel()
        {
            Comments = new List<CommentViewModel>();
            Genres = new List<GenreViewModel>();
            PlatformTypes = new List<PlatformTypeViewModel>();
        }
    }
}