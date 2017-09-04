using GameStore.Authentification.Abstract;
using GameStore.Common.App_LocalResources;
using GameStore.Common.Entities;

namespace GameStore.Authentification.Infrastructure
{
	public class UserIdentityProvider : IUserIdentityProvider
	{
		public User User { get; set; }

		public string Name => User != null ? User.Login : GlobalResource.Guest;

		public string AuthenticationType => typeof(User).ToString();

		public bool IsAuthenticated => User != null;
	}
}