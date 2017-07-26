using MongoDB.Driver;
using Ninject.Modules;

namespace GameStore.Common.Modules
{
	public class DalModule : NinjectModule
	{
		private readonly string _connectionString;
		private readonly string _databaseName;

		public DalModule(string connectionString, string databaseName)
		{
			_connectionString = connectionString;
			_databaseName = databaseName;
		}

		public override void Load()
		{
			Bind<IMongoDatabase>().ToMethod(ctx => new MongoClient(_connectionString).GetDatabase(_databaseName));
		}
	}
}