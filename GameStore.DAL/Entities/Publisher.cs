using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        [MaxLength(40)]
        public string CompanyName { get; set; }

        [Column(TypeName = "NTEXT")]
        public string Description { get; set; }

        [Column(TypeName = "NTEXT")]
        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
