using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using GameStore.DAL.Abstract;

namespace GameStore.Services.DTOs
{
	public class PlatformTypeDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

		public IEnumerable<GameDto> Games { get; set; }

		public bool IsDeleted { get; set; }

        public PlatformTypeDto()
        {
            Games = new List<GameDto>();
        }
    }
}
