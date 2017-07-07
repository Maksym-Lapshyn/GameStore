using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlatformTypeDto> GetAll()
        {
            var platforms = _unitOfWork.PlatformTypeRepository.Get();
            var platformDtos = Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(platforms);

            return platformDtos;
        }

        public IEnumerable<PlatformTypeDto> GetBy(int gameId)
        {
            var allPlatformTypes= _unitOfWork.PlatformTypeRepository.Get();
            var matchedPlatformTypes = (from type in allPlatformTypes from game in type.Games where game.Id == gameId select type).ToList();
            var platformTypeDtos = Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(matchedPlatformTypes);

            return platformTypeDtos;
        }
    }
}
