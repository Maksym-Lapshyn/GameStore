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
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IPlatformTypeRepository _platformTypeRepository;

		public PlatformTypeService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IPlatformTypeRepository platformTypeRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_platformTypeRepository = platformTypeRepository;
		}

		public IEnumerable<PlatformTypeDto> GetAll()
		{
			var platforms = _platformTypeRepository.Get();
			var platformDtos = _mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(platforms);

			return platformDtos;
		}
	}
}