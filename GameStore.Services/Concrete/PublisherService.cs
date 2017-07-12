using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PublisherService : IPublisherService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PublisherService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void Create(PublisherDto publisherDto)
		{
			var publisher = Mapper.Map<PublisherDto, Publisher>(publisherDto);
			_unitOfWork.PublisherRepository.Insert(publisher);
			_unitOfWork.Save();
		}

		public PublisherDto GetSingleBy(int publisherId)
		{
			var publisher = _unitOfWork.PublisherRepository.Get(publisherId);
			var publisherDto = Mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}

		public IEnumerable<PublisherDto> GetAll()
		{
			var publishers = _unitOfWork.PublisherRepository.Get();
			var publisherDtos = Mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherDto>>(publishers);

			return publisherDtos;
		}

		public PublisherDto GetSingleBy(string companyName)
		{
			var publisher = _unitOfWork.PublisherRepository
				.Get().First(p => p.CompanyName == companyName);
			var publisherDto = Mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}
	}
}
