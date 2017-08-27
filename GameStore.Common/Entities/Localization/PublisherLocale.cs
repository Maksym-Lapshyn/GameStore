namespace GameStore.Common.Entities.Localization
{
    public class PublisherLocale : BaseEntity
    {
        public string Description { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual Language Language { get; set; }
    }
}