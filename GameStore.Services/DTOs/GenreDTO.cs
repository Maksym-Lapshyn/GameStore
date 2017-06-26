using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GameStore.DAL.Abstract;

namespace GameStore.Services.DTOs
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GenreDto> ChildGenres { get; set; }
        public bool IsDeleted { get; set; }
        [ScriptIgnore]
        public virtual GenreDto ParentGenre { get; set; }

        public GenreDto()
        {
            ChildGenres = new List<GenreDto>();
        }
    }
}
