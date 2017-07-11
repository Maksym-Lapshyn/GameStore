namespace GameStore.Web.Infrastructure.Abstract
{
    public interface IFilter<T>
    {
        T Execute(T input);
    }
}
