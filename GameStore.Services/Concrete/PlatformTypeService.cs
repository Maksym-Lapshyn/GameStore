using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;

namespace GameStore.Services.Concrete
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PlatformTypeDto entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(PlatformTypeDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PlatformTypeDto Get(int id)
        {
            throw new NotImplementedException();
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
            var matchedPlatformTypes = new List<PlatformType>();
            foreach (var type in allPlatformTypes)
            {
                foreach (var game in type.Games)
                {
                    if (game.Id == gameId)
                    {
                        matchedPlatformTypes.Add(type);
                    }
                }
            }

            var platformTypeDtos = Mapper.Map<IEnumerable<PlatformType>, IEnumerable<PlatformTypeDto>>(matchedPlatformTypes);

            return platformTypeDtos;
        }
    }
}
