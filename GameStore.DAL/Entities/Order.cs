using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
