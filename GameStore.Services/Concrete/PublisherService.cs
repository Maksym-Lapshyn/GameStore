using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract.EntityFramework;

namespace GameStore.Services.Concrete
{
	public class PublisherService : IPublisherService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IEfPublisherRepository _publisherRepository;

		public PublisherService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IEfPublisherRepository publisherRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_publisherRepository = publisherRepository;
		}

		public void Create(PublisherDto publisherDto)
		{
			var publisher = _mapper.Map<PublisherDto, Publisher>(publisherDto);
			_publisherRepository.Insert(publisher);
			_unitOfWork.Save();
		}

		public PublisherDto GetSingleBy(string companyName)
		{
			var publisher = _publisherRepository.Get(companyName);
			var publisherDto = _mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}

		public IEnumerable<PublisherDto> GetAll()
		{
			var publishers = _publisherRepository.Get();
			var publisherDtos = _mapper.Map<IQueryable<Publisher>, IEnumerable<PublisherDto>>(publishers);

			return publisherDtos;
		}

		public void Update(PublisherDto publisherDto)
		{
			var publisher = _publisherRepository.Get(publisherDto.CompanyName);
			_mapper.Map(publisherDto, publisher);
			_publisherRepository.Update(publisher);
			_unitOfWork.Save();
		}
	}
}
