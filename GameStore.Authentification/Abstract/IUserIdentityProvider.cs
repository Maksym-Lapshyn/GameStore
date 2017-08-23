using System.Security.Principal;

namespace GameStore.Authentification.Abstract
{
	public interface IUserIdentityProvider : IIdentity, IUserProvider
	{
	}
}