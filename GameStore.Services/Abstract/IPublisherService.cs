using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface IPublisherService
	{
		PublisherDto GetSingle(string companyName);

		void Create(PublisherDto publisherDto);

		IEnumerable<PublisherDto> GetAll();

		void Update(PublisherDto publisherDto);

		void Delete(string companyName);
	}
}