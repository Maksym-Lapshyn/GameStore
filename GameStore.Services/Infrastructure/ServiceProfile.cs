using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Infrastructure;
using GameStore.Services.Dtos;
using System.Linq;

namespace GameStore.Services.Infrastructure
{
	public class ServiceProfile : Profile
	{
		public ServiceProfile()
		{
			CreateMap<Game, GameDto>()
				.ForMember(dto => dto.PublisherInput, option => option.MapFrom(entity => !entity.IsDeleted ? entity.Publisher.CompanyName : null))
				.ForMember(dto => dto.PlatformTypesInput,
					option => option.MapFrom(entity => !entity.IsDeleted ? entity.PlatformTypes.Select(platformType =>  platformType.Type).ToList() : null))
				.ForMember(dto => dto.GenresInput,
					option => option.MapFrom(entity => !entity.IsDeleted ? entity.Genres.Select(genre => genre.Name).ToList() : null));

			CreateMap<GameDto, Game>();

			CreateMap<Genre, GenreDto>()
				.ForMember(dto => dto.ParentGenreInput, option => option.MapFrom(entity => !entity.IsDeleted && entity.ParentGenre.Name != entity.Name ? entity.Name : null));

			CreateMap<GenreDto, Genre>();

			CreateMap<User, UserDto>()
				.ForMember(dto => dto.RolesInput, option => option.MapFrom(entity => !entity.IsDeleted ? entity.Roles.Select(role => role.Name) : null));

			CreateMap<UserDto, User>();

			CreateMap<CommentDto, Comment>().ReverseMap();

			CreateMap<PublisherDto, Publisher>().ReverseMap();

			CreateMap<PlatformTypeDto, PlatformType>().ReverseMap();

			CreateMap<OrderDto, Order>().ReverseMap();

			CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();

			CreateMap<GameFilterDto, GameFilter>().ReverseMap();

			CreateMap<OrderFilterDto, OrderFilter>().ReverseMap();

			CreateMap<RoleDto, Role>().ReverseMap();
		}
	}
}