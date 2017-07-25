using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using GameStore.DAL.Context;
using GameStore.DAL.Entities;
using MongoDB.Driver;
using Ninject.Modules;

namespace GameStore.DAL.Infrastructure
{
	public class DalModule : NinjectModule
	{
		private readonly string _connectionString;

		public DalModule(string connectionString)
		{
			_connectionString = connectionString;
		}

		public override void Load()
		{
			Bind<MongoClient>().ToSelf().WithConstructorArgument("connectionString");
			Bind<IMongoDatabase>().ToMethod()
		}
	}
}
