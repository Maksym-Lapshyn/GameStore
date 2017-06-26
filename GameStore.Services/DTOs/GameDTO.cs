using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;

namespace GameStore.Services.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CommentDto> Comments { get; set; }
        public virtual ICollection<GenreDto> Genres { get; set; }
        public virtual ICollection<PlatformTypeDto> PlatformTypes { get; set; }
        public bool IsDeleted { get; set; }

        public GameDto()
        {
            Comments = new List<CommentDto>();
            Genres = new List<GenreDto>();
            PlatformTypes = new List<PlatformTypeDto>();
        }
    }
}
