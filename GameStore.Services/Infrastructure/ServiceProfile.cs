
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
				.ForMember(dto => dto.PlatformTypesData, options => options.MapFrom(entity => entity.PlatformTypes))
				.ForMember(dto => dto.GenresData, options => options.MapFrom(entity => entity.Genres))
				.ForMember(dto => dto.CommentsCount, options => options.MapFrom(entity => entity.Comments.Count))
				.ForMember(dto => dto.PublishersData, option => option.MapFrom(entity => new List<PublisherDto> { Mapper.Map<Publisher, PublisherDto>(entity.Publisher) }));

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