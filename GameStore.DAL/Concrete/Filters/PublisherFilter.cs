using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Concrete.Filters
{
	public class PublisherFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<string> _publisherCompanyNames;

		public PublisherFilter(IEnumerable<string> publisherCompanyNames)
		{
			_publisherCompanyNames = publisherCompanyNames;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(g => _publisherCompanyNames.Contains(g.Publisher.CompanyName));
		}
	}
}