using GameStore.Common.Entities;

namespace GameStore.Services.Dtos
{
	public class ImageDto : BaseEntity
	{
		public string Name { get; set; }

		public byte[] Data { get; set; }

		public string MimeType { get; set; }

		public virtual GameDto Game { get; set; }
	}
}