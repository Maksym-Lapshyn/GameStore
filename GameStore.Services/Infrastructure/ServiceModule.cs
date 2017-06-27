using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using Ninject.Modules;

namespace GameStore.Services.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
		//TODO: Consider: make fields readonly Fixed in ML_2
		private string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}
