using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
	public class PlatformType : BaseEntity
    {
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public PlatformType()
        {
            Games = new List<Game>();
        }
    }
}
