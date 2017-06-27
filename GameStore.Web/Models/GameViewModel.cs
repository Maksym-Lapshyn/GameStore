using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Web.Models
{
    public class GameViewModel
    {
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
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<GenreViewModel> Genres { get; set; }
        public ICollection<PlatformTypeViewModel> PlatformTypes { get; set; }
        public bool IsDeleted { get; set; }

        public GameViewModel()
        {
            Comments = new List<CommentViewModel>();
            Genres = new List<GenreViewModel>();
            PlatformTypes = new List<PlatformTypeViewModel>();
        }
    }
}