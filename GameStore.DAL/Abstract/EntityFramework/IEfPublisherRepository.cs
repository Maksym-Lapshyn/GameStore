using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.DAL.Abstract.EntityFramework
{
	public interface IEfPublisherRepository
	{
		Publisher GetSingle(string companyName);

		IQueryable<Publisher> GetAll();

		bool Contains(string companyName);

		void Insert(Publisher publisher);

		void Update(Publisher publisher);
	}
}