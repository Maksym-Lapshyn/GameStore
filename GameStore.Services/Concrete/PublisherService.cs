using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract.Localization;

namespace GameStore.Services.Concrete
{
	public class PublisherService : IPublisherService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IInputLocalizer<Publisher> _inputLocalizer;
		private readonly IOutputLocalizer<Publisher> _outputLocalizer;
		private readonly IPublisherRepository _publisherRepository;

		public PublisherService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IInputLocalizer<Publisher> inputLocalizer,
			IOutputLocalizer<Publisher> outputLocalizer,
			IPublisherRepository publisherRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_inputLocalizer = inputLocalizer;
			_outputLocalizer = outputLocalizer;
			_publisherRepository = publisherRepository;
		}

		public void Create(string language, PublisherDto publisherDto)
		{
			var publisher = _mapper.Map<PublisherDto, Publisher>(publisherDto);
			_inputLocalizer.Localize(language, publisher);
			_publisherRepository.Insert(publisher);
			_unitOfWork.Save();
		}

		public PublisherDto GetSingle(string language, string companyName)
		{
			var publisher = _publisherRepository.GetSingle(p => p.CompanyName == companyName);
			_outputLocalizer.Localize(language, publisher);
			var publisherDto = _mapper.Map<Publisher, PublisherDto>(publisher);

			return publisherDto;
		}

		public IEnumerable<PublisherDto> GetAll(string language)
		{
			var publishers = _publisherRepository.GetAll().ToList();

			foreach (var publisher in publishers)
			{
				_outputLocalizer.Localize(language, publisher);
			}

			var publisherDtos = _mapper.Map<IEnumerable<Publisher>, IEnumerable<PublisherDto>>(publishers);

			return publisherDtos;
		}

		public void Update(string language, PublisherDto publisherDto)
		{
			var publisher = _publisherRepository.GetSingle(p => p.Id == publisherDto.Id);
			_mapper.Map(publisherDto, publisher);
			_inputLocalizer.Localize(language, publisher);
			_publisherRepository.Update(publisher);
			_unitOfWork.Save();
		}

		public void Delete(string companyName)
		{
			_publisherRepository.Delete(companyName);
			_unitOfWork.Save();
		}

		public bool Contains(string companyName)
		{
			return _publisherRepository.Contains(p => p.CompanyName == companyName);
		}
	}
}