using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete
{
	public class PublisherCloner : ICloner<Publisher>
	{
		private readonly IEfPublisherRepository _publisherRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PublisherCloner(IEfPublisherRepository publisherRepository, IUnitOfWork unitOfWork)
		{
			_publisherRepository = publisherRepository;
			_unitOfWork = unitOfWork;
		}

		public Publisher Clone(Publisher publisher)
		{
			if (!_publisherRepository.Contains(publisher.CompanyName))
			{
				publisher.Games?.Clear();
				_publisherRepository.Insert(publisher);
				_unitOfWork.Save();
			}

			return publisher.Id != default(int) ? publisher : _publisherRepository.GetSingle(publisher.CompanyName);
		}
	}
}