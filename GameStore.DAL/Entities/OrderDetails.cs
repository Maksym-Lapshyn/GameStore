using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class OrderDetails : BaseEntity
    {
        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        public decimal Price { get; set; }

        //[Column(TypeName = "smallint")]
        public short Quantity { get; set; }

        //[Column(TypeName = "real")]
        public float Discount { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}