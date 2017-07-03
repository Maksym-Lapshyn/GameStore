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

        //[Column(TypeName = "money")]
        public decimal Price { get; set; }

        //[Column(TypeName = "smallint")]
        public short UnitsInStock { get; set; }

        //[Column(TypeName = "bit")]
        public bool Discontinued { get; set; }

        public int? PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
    }
}
