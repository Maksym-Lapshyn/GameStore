using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
	public class Game : BaseEntity
    {
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<PlatformType> PlatformTypes { get; set; }

        public Game()
        {
            Comments = new List<Comment>();
            Genres = new List<Genre>();
            PlatformTypes = new List<PlatformType>();
        }
    }
}
