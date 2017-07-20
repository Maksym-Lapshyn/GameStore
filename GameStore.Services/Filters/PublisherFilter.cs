using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class PublisherFilter : IFilter<IQueryable<Game>>
	{
		private readonly IEnumerable<int> _publisherIds;

		public PublisherFilter(IEnumerable<int> publisherIds)
		{
			_publisherIds = publisherIds;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			return input.Where(g => _publisherIds.Contains(g.PublisherId.Value)); //TODO Consider: simplify to 'return input.Where....'
		}
	}
}