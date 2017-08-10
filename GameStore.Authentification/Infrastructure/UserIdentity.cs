using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using System.Security.Principal;

namespace GameStore.Authentification.Infrastructure
{
	public class UserIdentity : IIdentity, IUserProvider
	{
		public User User { get; set; }

		public string Name => User?.Name;

		public string AuthenticationType => typeof(User).ToString();

		public bool IsAuthenticated => User != null;
	}
}