namespace GameStore.DAL.Abstract
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}