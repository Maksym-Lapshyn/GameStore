using System;
using System.Collections.Generic;

namespace GameStore.DAL.Entities
{
	public class Order : BaseEntity
	{
		public string CustomerId { get; set; }

		public DateTime OrderDate { get; set; }

		public virtual ICollection<OrderDetails> OrderDetails { get; set; }
	}
}