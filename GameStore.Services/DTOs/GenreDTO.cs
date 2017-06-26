using System.Collections.Generic;
using System.Web.Script.Serialization;

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
