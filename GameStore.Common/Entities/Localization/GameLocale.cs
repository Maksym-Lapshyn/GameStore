namespace GameStore.Common.Entities.Localization
{
	public class GameLocale : BaseEntity
	{
		public string Description { get; set; }

		public virtual Game Game { get; set; }

		public virtual Language Language { get; set; }
	}
}