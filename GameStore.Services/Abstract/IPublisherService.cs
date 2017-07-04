using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface IPublisherService : IService<PublisherDto>
    {
        PublisherDto GetBy(string companyName);
    }
}
