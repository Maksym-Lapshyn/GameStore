using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IPublisherService : IService
	{
		PublisherDto GetSingleBy(string companyName);

		void Create(PublisherDto publisherDto);

		IEnumerable<PublisherDto> GetAll();
	}
}