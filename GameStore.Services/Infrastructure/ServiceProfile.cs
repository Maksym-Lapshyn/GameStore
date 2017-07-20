
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

			CreateMap<CommentDto, Comment>().ReverseMap();

			CreateMap<PublisherDto, Publisher>().ReverseMap();

			CreateMap<PlatformTypeDto, PlatformType>().ReverseMap();

			CreateMap<GenreDto, Genre>().ReverseMap();

			CreateMap<OrderDto, Order>().ReverseMap();

			CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
		}
	}
}