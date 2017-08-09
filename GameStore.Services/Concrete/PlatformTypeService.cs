using AutoMapper;
using GameStore.DAL.Abstract.Common;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Concrete
{
	public class PlatformTypeService : IPlatformTypeService
	{
		private readonly IMapper _mapper;
		private readonly IPlatformTypeRepository _platformTypeRepository;

		public PlatformTypeService(IMapper mapper,
			IPlatformTypeRepository platformTypeRepository)
		{
			_mapper = mapper;
			_platformTypeRepository = platformTypeRepository;
		}

		public IEnumerable<PlatformTypeDto> GetAll()
		{
			var platforms = _platformTypeRepository.GetAll();
			var platformDtos = _mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(platforms);

			return platformDtos;
		}
	}
}