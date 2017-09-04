namespace GameStore.Common.Abstract
{
	public interface IHashGenerator<in T>
	{
		string Generate(T input);
	}
}