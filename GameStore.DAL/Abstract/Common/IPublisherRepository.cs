using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IPublisherRepository
	{
		Publisher GetSingle(string companyName);

		IEnumerable<Publisher> GetAll();

		bool Contains(string companyName);

		void Insert(Publisher publisher);

		void Update(Publisher publisher);

		void Delete(string companyName);
	}
}