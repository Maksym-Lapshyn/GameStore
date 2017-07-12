using GameStore.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Entities;

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
			input = input.Where(g => _publisherIds.Contains(g.PublisherId.Value));

			return input;
		}
	}
}
