using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class SubGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
    }
}
