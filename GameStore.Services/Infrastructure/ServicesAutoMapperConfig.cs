using AutoMapper;
using GameStore.DAL.Entities;
using GameStore.Services.DTOs;

namespace GameStore.Services.Infrastructure
{
    public class ServicesAutoMapperConfig
	{
		public static void RegisterMappings()
		{
			Mapper.Initialize(cfg => cfg.CreateMap<GameDto, Game>());
			Mapper.Initialize(cfg => cfg.CreateMap<Game, GameDto>()
				.ForMember(e => e.PlatformTypesData, o => o.MapFrom(d => d.PlatformTypes))
				.ForMember(e => e.GenresData, o => o.MapFrom(d => d.Genres)));
			Mapper.Initialize(cfg => cfg.CreateMap<CommentDto, Comment>());
			Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDto>());
			Mapper.Initialize(cfg => cfg.CreateMap<PublisherDto, Publisher>());
			Mapper.Initialize(cfg => cfg.CreateMap<Publisher, PublisherDto>());
			Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeDto, PlatformType>());
			Mapper.Initialize(cfg => cfg.CreateMap<PlatformType, PlatformTypeDto>());
			Mapper.Initialize(cfg => cfg.CreateMap<GenreDto, Genre>());
			Mapper.Initialize(cfg => cfg.CreateMap<Genre, GenreDto>());
			Mapper.Initialize(cfg => cfg.CreateMap<OrderDto, Order>());
			Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDto>());
			Mapper.Initialize(cfg => cfg.CreateMap<OrderDetailsDto, OrderDetails>());
			Mapper.Initialize(cfg => cfg.CreateMap<OrderDetails, OrderDetailsDto>());
			Mapper.Initialize(cfg => cfg.CreateMissingTypeMaps = true);
			Mapper.AssertConfigurationIsValid();
		}
	}
}