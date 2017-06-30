using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace GameStore.Services.DTOs
{
	public class PlatformTypeDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

		public IEnumerable<GameDto> Games { get; set; }

        public PlatformTypeDto()
        {
            Games = new List<GameDto>();
        }
    }
}
