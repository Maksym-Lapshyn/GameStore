using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Services.Enums;
using GameStore.Services.Filters;

namespace GameStore.Services.Concrete
{
	public class GamePipeline : IPipeline<IQueryable<Game>>
	{
		private readonly List<IFilter<IQueryable<Game>>> _filters = new List<IFilter<IQueryable<Game>>>();

		public void ApplyFilter(FilterDto model)
		{
			if (model.GenresInput.Count != 0)
			{
				Register(new GenreFilter(model.GenresInput));
			}

			if (model.PlatformTypesInput.Count != 0)
			{
				Register(new PlatformTypeFilter(model.PlatformTypesInput));
			}

			if (model.PublishersInput.Count != 0)
			{
				Register(new PublisherFilter(model.PublishersInput));
			}

			if (model.MinPrice != default(decimal))
			{
				Register(new MinPriceFilter(model.MinPrice));
			}

			if (model.MaxPrice != default(decimal))
			{
				Register(new MaxPriceFilter(model.MaxPrice));
			}

			if (model.DateOptions != DateOptions.None)
			{
				Register(new DateOptionsFilter(model.DateOptions));
			}

			if (model.GameName != null)
			{
				Register(new GameNameFilter(model.GameName));
			}

			Register(new SortOptionsFilter(model.SortOptions));

			/*if (model.Paginator.PageSize != 0)
			{
				Register(new PaginationFilter(model.Paginator));
			}*/
		}

		public IPipeline<IQueryable<Game>> Register(IFilter<IQueryable<Game>> filter)
		{
			_filters.Add(filter);

			return this;
		}

		public IQueryable<Game> Process(IQueryable<Game> input)
		{
			_filters.ForEach(f => input = f.Execute(input));

			return input;
		}
	}
}