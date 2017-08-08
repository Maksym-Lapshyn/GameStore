using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Concrete.Common;
using Ninject.Modules;

namespace GameStore.Ninject.Modules
{
	public class AuthenticationModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserRepository>().To<UserRepository>();
		}
	}
}