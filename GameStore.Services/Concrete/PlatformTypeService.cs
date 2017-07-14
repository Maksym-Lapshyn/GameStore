using AutoMapper;
using GameStore.DAL.Abstract;
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

		public PlatformTypeService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<PlatformTypeDto> GetAll()
		{
			var platforms = _unitOfWork.PlatformTypeRepository.Get();
			var platformDtos = _mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(platforms);

			return platformDtos;
		}
	}
}
