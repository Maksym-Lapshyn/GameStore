namespace GameStore.Common.Entities.Localization
{
	public class PlatformTypeLocale : BaseEntity
	{
		public string Type { get; set; }

		public virtual PlatformType PlatformType { get; set; }

		public virtual Language Language { get; set; }
	}
}