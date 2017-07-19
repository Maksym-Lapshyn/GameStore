
using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.Services.DTOs;
using System.Collections.Generic;

namespace GameStore.Services.Infrastructure
{
	public class ServiceProfile : Profile
	{
		public ServiceProfile()
		{
			CreateMap<GameDto, Game>();

			CreateMap<Game, GameDto>() //TODO Suggestion: use more meaningful variables instead of d,o,e
				.ForMember(d => d.PlatformTypesData, o => o.MapFrom(e => e.PlatformTypes))
				.ForMember(d => d.GenresData, o => o.MapFrom(e => e.Genres))
				.ForMember(d => d.CommentsCount, o => o.MapFrom(e => e.Comments.Count))
				.ForMember(d => d.PublishersData, o => o.MapFrom(e => new List<PublisherDto> { Mapper.Map<Publisher, PublisherDto>(e.Publisher) }));

			CreateMap<CommentDto, Comment>();

			CreateMap<Comment, CommentDto>();

			CreateMap<PublisherDto, Publisher>();

			CreateMap<Publisher, PublisherDto>();

			CreateMap<PlatformTypeDto, PlatformType>();

			CreateMap<PlatformType, PlatformTypeDto>();

			CreateMap<GenreDto, Genre>();

			CreateMap<Genre, GenreDto>();

			CreateMap<OrderDto, Order>();

			CreateMap<Order, OrderDto>();

			CreateMap<OrderDetailsDto, OrderDetails>();

			CreateMap<OrderDetails, OrderDetailsDto>();
		}
	}
}