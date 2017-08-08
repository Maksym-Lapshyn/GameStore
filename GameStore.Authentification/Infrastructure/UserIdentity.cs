using GameStore.Authentification.Abstract;
using GameStore.Common.Entities;
using System.Security.Principal;

namespace GameStore.Authentification.Infrastructure
{
	public class UserIdentity : IIdentity, IUserProvider
	{
		private const string DefaultUserName = "Guest";

		public User User { get; set; }

		public string Name => User != null ? User.Name : DefaultUserName;

		public string AuthenticationType => typeof(User).ToString();

		public bool IsAuthenticated => User != null;
	}
}