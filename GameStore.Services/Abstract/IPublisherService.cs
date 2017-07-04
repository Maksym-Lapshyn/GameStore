using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IPublisherService
    {
        PublisherDto GetSingleBy(string companyName);

        void Create(PublisherDto entity);

        PublisherDto GetSingleBy(int publisherId);

        IEnumerable<PublisherDto> GetAll();
    }
}
