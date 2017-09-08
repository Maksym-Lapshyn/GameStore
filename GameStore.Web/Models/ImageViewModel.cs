using GameStore.Common.Entities;

namespace GameStore.Web.Models
{
	public class ImageViewModel : BaseEntity
	{
		public string Name { get; set; }

		public byte[] Data { get; set; }

		public string MimeType { get; set; }

		public virtual GameViewModel Game { get; set; }
	}
}