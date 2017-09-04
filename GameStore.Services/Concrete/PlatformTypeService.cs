using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PlatformTypeService : IPlatformTypeService
	{
		private readonly IMapper _mapper;
		private readonly IOutputLocalizer<PlatformType> _outputLocalizer;
		private readonly IPlatformTypeRepository _platformTypeRepository;

		public PlatformTypeService(IMapper mapper,
			IOutputLocalizer<PlatformType> outputLocalizer,
			IPlatformTypeRepository platformTypeRepository)
		{
			_mapper = mapper;
			_outputLocalizer = outputLocalizer;
			_platformTypeRepository = platformTypeRepository;
		}

		public IEnumerable<PlatformTypeDto> GetAll(string language)
		{
			var platformTypes = _platformTypeRepository.GetAll().ToList();

			foreach (var platformType in platformTypes)
			{
				_outputLocalizer.Localize(language, platformType);
			}

			var platformDtos = _mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(platformTypes);

			return platformDtos;
		}
	}
}