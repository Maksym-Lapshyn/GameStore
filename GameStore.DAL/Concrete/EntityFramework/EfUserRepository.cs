using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using System;

namespace GameStore.DAL.Concrete.EntityFramework
{
	public class EfUserRepository : IEfUserRepository
	{
		public User GetSingle(string username, string password)
		{
			throw new NotImplementedException();
		}

		public bool Contains(string username, string password)
		{
			throw new NotImplementedException();
		}
	}
}