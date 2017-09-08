namespace GameStore.Common.Entities
{
	public class Image : BaseEntity
	{
		public string Name { get; set; }

		public byte[] Data { get; set; }

		public string MimeType { get; set; }

		public virtual Game Game { get; set; }
	}
}