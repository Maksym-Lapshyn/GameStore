using GameStore.Common.Entities;

namespace GameStore.Authentification.Abstract
{
	public interface IUserProvider
	{
		User User { get; set; }
	}
}