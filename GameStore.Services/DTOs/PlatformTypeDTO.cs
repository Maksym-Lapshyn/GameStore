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
	public class PlatformTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        [ScriptIgnore] //TODO: Required: Remove this attribute
		public virtual ICollection<GameDto> Games { get; set; } //TODO: Consider: Remove 'virtual' and ICollection to IEnumerable
		public bool IsDeleted { get; set; }

        public PlatformTypeDto()
        {
            Games = new List<GameDto>();
        }
    }
}
