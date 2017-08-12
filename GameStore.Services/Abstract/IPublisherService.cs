using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IPublisherService
	{
		PublisherDto GetSingle(string companyName);

		void Create(PublisherDto publisherDto);

		IEnumerable<PublisherDto> GetAll();

		void Update(PublisherDto publisherDto);

		void Delete(string companyName);

		bool Contains(string gameKey);
	}
}