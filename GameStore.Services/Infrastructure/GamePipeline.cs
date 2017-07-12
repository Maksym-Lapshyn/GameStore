using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.Services.Enums;
using System.Collections.Generic;
using System.Linq;
using GameStore.Services.Filters;

namespace GameStore.Services.Infrastructure
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

			if (model.PublishersData.Count != 0)
			{
				Register(new PublisherFilter(model.PublishersInput));
			}

			if (model.SortOptions != SortOptions.None)
			{
				Register(new SortOptionsFilter(model.SortOptions));
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

			if (model.GameName != string.Empty)
			{
				Register(new GameNameFilter(model.GameName));
			}
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