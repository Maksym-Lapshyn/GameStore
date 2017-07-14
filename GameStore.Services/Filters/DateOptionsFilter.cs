
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.Enums;
using System;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class DateOptionsFilter : IFilter<IQueryable<Game>>
	{
		private readonly DateOptions _dateOption;

		public DateOptionsFilter(DateOptions dateOption)
		{
			_dateOption = dateOption;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			switch(_dateOption)
			{
				case DateOptions.LastWeek:
					input = input.Where(g => (DateTime.UtcNow - g.DatePublished).Days < 7);
					break;
				case DateOptions.LastMonth:
					input = input.Where(g => (DateTime.UtcNow - g.DatePublished).Days < 30);
					break;
				case DateOptions.LastYear:
					input = input.Where(g => (DateTime.UtcNow - g.DatePublished).Days < 365);
					break;
				case DateOptions.TwoYears:
					input = input.Where(g => (DateTime.UtcNow - g.DatePublished).Days < 730);
					break;
				case DateOptions.ThreeYears:
					input = input.Where(g => (DateTime.UtcNow - g.DatePublished).Days < 1095);
					break;
			}

			return input;
		}
	}
}
