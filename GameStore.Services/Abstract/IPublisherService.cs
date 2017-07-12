using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IPublisherService
	{
		PublisherDto GetSingleBy(string companyName);

		void Create(PublisherDto publisherDto);

		PublisherDto GetSingleBy(int publisherId);

		IEnumerable<PublisherDto> GetAll();
	}
}