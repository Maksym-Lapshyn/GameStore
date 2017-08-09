using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Concrete
{
	public class PublisherCopier : ICopier<Publisher>
	{
		private readonly IEfPublisherRepository _publisherRepository;
		private readonly IUnitOfWork _unitOfWork;

		public PublisherCopier(IEfPublisherRepository publisherRepository, IUnitOfWork unitOfWork)
		{
			_publisherRepository = publisherRepository;
			_unitOfWork = unitOfWork;
		}

		public Publisher Copy(Publisher publisher)
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