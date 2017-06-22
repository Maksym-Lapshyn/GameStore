using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.DTOs
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CommentDTO> Comments { get; set; }
        public virtual ICollection<GenreDTO> Genres { get; set; }
        public virtual ICollection<PlatformTypeDTO> PlatformTypes { get; set; }
        public bool IsDeleted { get; set; }

        public GameDTO()
        {
            Comments = new List<CommentDTO>();
            Genres = new List<GenreDTO>();
            PlatformTypes = new List<PlatformTypeDTO>();
        }
    }
}
