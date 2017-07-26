using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Abstract.EntityFramework;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class PlatformTypeService : IPlatformTypeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IEfPlatformTypeRepository _platformTypeRepository;

		public PlatformTypeService(IUnitOfWork unitOfWork,
			IMapper mapper,
			IEfPlatformTypeRepository platformTypeRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_platformTypeRepository = platformTypeRepository;
		}

		public IEnumerable<PlatformTypeDto> GetAll()
		{
			var platforms = _platformTypeRepository.Get();
			var platformDtos = _mapper.Map<IQueryable<PlatformType>, IEnumerable<PlatformTypeDto>>(platforms);

			return platformDtos;
		}
	}
}