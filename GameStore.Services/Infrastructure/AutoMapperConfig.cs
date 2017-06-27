using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.DAL.Entities;

namespace GameStore.Services.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<GameDto, Game>());
            Mapper.Initialize(cfg => cfg.CreateMap<Game, GameDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<CommentDto, Comment>());
            Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeDto, PlatformType>());
            Mapper.Initialize(cfg => cfg.CreateMap<PlatformType, PlatformTypeDto>());
            Mapper.Initialize(cfg => cfg.CreateMap<GenreDto, Genre>());
            Mapper.Initialize(cfg => cfg.CreateMap<Genre, GenreDto>());
            Mapper.Initialize(cfg => cfg.CreateMissingTypeMaps = true);
            Mapper.AssertConfigurationIsValid();
        }
    }
}
