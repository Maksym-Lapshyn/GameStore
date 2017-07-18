
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
			var date = default(DateTime);

			switch(_dateOption)
			{
				case DateOptions.LastWeek:
					date = DateTime.UtcNow.AddDays(-7);
					break;
				case DateOptions.LastMonth:
					date = DateTime.UtcNow.AddDays(-30);
					break;
				case DateOptions.LastYear:
					date = DateTime.UtcNow.AddDays(-365);
					break;
				case DateOptions.TwoYears:
					date = DateTime.UtcNow.AddDays(-730);
					break;
				case DateOptions.ThreeYears:
					date = DateTime.UtcNow.AddDays(-1095);
					break;
			}

			input = input.Where(g => date <= g.DatePublished);

			return input;
		}
	}
}
