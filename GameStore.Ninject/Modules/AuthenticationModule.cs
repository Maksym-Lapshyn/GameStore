using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Concrete.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Concrete.EntityFramework;
using Ninject.Modules;

namespace GameStore.Ninject.Modules
{
	public class AuthenticationModule : NinjectModule
	{
		public override void Load()
		{
			//Bind<IUserRepository>().To<UserRepository>();
            //Bind<IEfUserRepository>().To<EfUserRepository>();
		}
	}
}