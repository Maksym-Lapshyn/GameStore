using GameStore.DAL.Entities;
using System.Collections.Generic;

namespace GameStore.DAL.Abstract.Common
{
	public interface IPublisherRepository
	{
		Publisher Get(string companyName);

		IEnumerable<Publisher> Get();

		bool Contains(string companyName);

		void Insert(Publisher publisher);

		void Update(Publisher publisher);
	}
}