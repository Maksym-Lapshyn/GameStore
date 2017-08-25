namespace GameStore.Common.Entities.Localization
{
	public class RoleLocale : BaseEntity
	{
		public string Name { get; set; }

		public virtual Role Role { get; set; }

		public virtual Language Language { get; set; }
	}
}