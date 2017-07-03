using System;
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
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PublisherDto publisherDto)
        {
            var publisher = Mapper.Map<PublisherDto, Publisher>(publisherDto);
            _unitOfWork.PublisherRepository.Insert(publisher);
            _unitOfWork.Save();
        }

        public void Edit(PublisherDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public PublisherDto Get(int id)
        {
            var publisher = _unitOfWork.PublisherRepository.GetById(id);
            var publisherDto = Mapper.Map<Publisher, PublisherDto>(publisher);

            return publisherDto;
        }

        public IEnumerable<PublisherDto> GetAll()
        {
            var publishers = _unitOfWork.PublisherRepository.Get();
            var publisherDtos = Mapper.Map<IEnumerable<Publisher>, IEnumerable< PublisherDto>>(publishers);

            return publisherDtos;
        }

        public PublisherDto GetBy(string companyName)
        {
            var publisher = _unitOfWork.PublisherRepository.Get(p => p.CompanyName == companyName).First();
            var publisherDto = Mapper.Map<Publisher, PublisherDto>(publisher);

            return publisherDto;
        }
    }
}
