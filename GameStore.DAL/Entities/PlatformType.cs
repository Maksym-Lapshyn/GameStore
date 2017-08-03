using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
	public class PlatformType : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		public string Type { get; set; }

		public ICollection<Game> Games { get; set; }
	}
}