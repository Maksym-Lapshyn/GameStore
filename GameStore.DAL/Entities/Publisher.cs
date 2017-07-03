using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        //[Column(TypeName = "varchar(40)")]
        public string CompanyName { get; set; }

        //[Column(TypeName = "ntext")]
        public string Description { get; set; }

        //[Column(TypeName = "ntext")]
        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
