namespace GameStore.Common.Abstract
{
	public interface IHasher<in T>
	{
		string GenerateHash(T input);
	}
}