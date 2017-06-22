using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GameStore.Services.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GenreDTO> ChildGenres { get; set; }
        public bool IsDeleted { get; set; }
        [ScriptIgnore]
        public virtual GenreDTO ParentGenre { get; set; }

        public GenreDTO()
        {
            ChildGenres = new List<GenreDTO>();
        }
    }
}
