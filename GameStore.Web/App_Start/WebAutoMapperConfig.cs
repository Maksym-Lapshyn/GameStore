using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.Web.Models;

namespace GameStore.Web.App_Start
{
    public static class WebAutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<GameDto, GameViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<GameViewModel, GameDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<CommentDto, CommentViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<CommentViewModel, CommentDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeDto, PlatformTypeViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeViewModel, PlatformTypeDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<GenreDto, GenreViewModel>());
            Mapper.Initialize(cfg => cfg.CreateMap<GenreViewModel, GenreDto>());
            Mapper.Initialize(cfg => cfg.CreateMissingTypeMaps = true);
            Mapper.AssertConfigurationIsValid();
        }
    }
}
