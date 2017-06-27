using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GameStore.DAL.Abstract;

namespace GameStore.Services.DTOs
{
	//TODO: Required: Blank line after each method/property
	public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GenreDto> ChildGenres { get; set; }
        public bool IsDeleted { get; set; }
        [ScriptIgnore] //TODO: Required: Remove this attribute
        public virtual GenreDto ParentGenre { get; set; } //TODO: Required: Remove 'virtual' and ICollection to IEnumerable

		public GenreDto()
        {
            ChildGenres = new List<GenreDto>();
        }
    }
}
