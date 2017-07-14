using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.Web.Models;

namespace GameStore.Web
{
	public class WebProfile : Profile
	{
		public WebProfile()
		{
			CreateMap<GameDto, GameViewModel>().ReverseMap();

			CreateMap<CommentDto, CommentViewModel>().ReverseMap();

			CreateMap<PublisherDto, PublisherViewModel>().ReverseMap();

			CreateMap<PlatformTypeDto, PlatformTypeViewModel>().ReverseMap();

			CreateMap<GenreDto, GenreViewModel>().ReverseMap();

			CreateMap<OrderDto, OrderViewModel>().ReverseMap();

			CreateMap<OrderDetailsDto, OrderDetailsViewModel>().ReverseMap();

			CreateMap<FilterDto, FilterViewModel>().ReverseMap();

			CreateMap<PaginatorDto, PaginatorViewModel>().ReverseMap();

			CreateMap<Services.Enums.DateOptions, Models.Enums.DateOptions>().ReverseMap();

			CreateMap<Services.Enums.SortOptions, Models.Enums.SortOptions>().ReverseMap();
		}
	}
}