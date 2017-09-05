using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface IPublisherService
	{
		PublisherDto GetSingle(string language, string companyName);

		PublisherDto GetSingleOrDefault(string language, string companyName);

		void Create(string language, PublisherDto publisherDto);

		IEnumerable<PublisherDto> GetAll(string language);

		PublisherDto GetSingleByGameKey(string language, string gameKey);

		void Update(string language, PublisherDto publisherDto);

		void Delete(string companyName);

		bool Contains(string gameKey);
	}
}