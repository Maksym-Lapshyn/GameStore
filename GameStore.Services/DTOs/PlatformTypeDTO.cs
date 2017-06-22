using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.DTOs
{
    public class PlatformTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<GameDTO> Games { get; set; }
        public bool IsDeleted { get; set; }

        public PlatformTypeDTO()
        {
            Games = new List<GameDTO>();
        }
    }
}
