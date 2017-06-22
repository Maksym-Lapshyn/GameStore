using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.Services.DTOs;
using GameStore.Domain.Entities;

namespace GameStore.Services.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<GameDTO, Game>());
            Mapper.Initialize(cfg => cfg.CreateMap<Game, GameDTO>());

            Mapper.Initialize(cfg => cfg.CreateMap<CommentDTO, Comment>());
            Mapper.Initialize(cfg => cfg.CreateMap<Comment, CommentDTO>());

            Mapper.Initialize(cfg => cfg.CreateMap<PlatformTypeDTO, PlatformType>());
            Mapper.Initialize(cfg => cfg.CreateMap<PlatformType, PlatformTypeDTO>());

            Mapper.Initialize(cfg => cfg.CreateMap<GenreDTO, Genre>());
            Mapper.Initialize(cfg => cfg.CreateMap<Genre, GenreDTO>());

            Mapper.Initialize(cfg => cfg.CreateMissingTypeMaps = true);

            Mapper.AssertConfigurationIsValid();
        }
    }
}
