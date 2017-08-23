using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;

namespace GameStore.Authentification.Infrastructure
{
	public class UserIdentityProvider : IUserIdentityProvider
	{
		public User User { get; set; }

		public string Name => User != null ? User.Login : "Guest";

		public string AuthenticationType => typeof(User).ToString();

		public bool IsAuthenticated => User != null;
	}
}