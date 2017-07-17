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
		private readonly IMapper _mapper;

		public PublisherService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public void Create(PublisherDto publisherDto)
		{
			var publisher = _mapper.Map<PublisherDto, Publisher>(publisherDto);
			_unitOfWork.PublisherRepository.Insert(publisher);
			_unitOfWork.Save();
		}

		public PublisherDto GetSingleBy(int publisherId)
		{
			var publisher = _unitOfWork.PublisherRepository.Get(publisherId);
			var publisherDto = _mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}

		public IEnumerable<PublisherDto> GetAll()
		{
			var publishers = _unitOfWork.PublisherRepository.Get();
			var publisherDtos = _mapper.Map<IQueryable<Publisher>, IEnumerable<PublisherDto>>(publishers);

			return publisherDtos;
		}

		public PublisherDto GetSingleBy(string companyName)
		{
			var publisher = _unitOfWork.PublisherRepository
				.Get().First(p => p.CompanyName == companyName);
			var publisherDto = _mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}
	}
}
