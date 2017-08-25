namespace GameStore.Common.Entities.Localization
{
	public class GenreLocale : BaseEntity
	{
		public string Name { get; set; }

		public string ParentGenreName { get; set; }

		public virtual Genre Genre { get; set; }

		public virtual Language Language { get; set; }
	}
}