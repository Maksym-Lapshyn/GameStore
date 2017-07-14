using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Linq;

namespace GameStore.Services.Filters
{
	public class PaginationFilter : IFilter<IQueryable<Game>>
	{
		private readonly PaginatorDto _paginator;

		public PaginationFilter(PaginatorDto paginator)
		{
			_paginator = paginator;
		}

		public IQueryable<Game> Execute(IQueryable<Game> input)
		{
			input = input.Skip(_paginator.PageSize * (_paginator.CurrentPage - 1)).Take(_paginator.PageSize);

			return input;
		}
	}
}