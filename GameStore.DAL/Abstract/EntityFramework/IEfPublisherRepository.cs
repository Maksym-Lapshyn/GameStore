using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPublisherRepository
	{
		Publisher Get(string companyName);

		IQueryable<Publisher> Get();

		bool Contains(string companyName);

		void Insert(Publisher publisher);

		void Update(Publisher publisher);
	}
}