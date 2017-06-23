using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Entities;
using GameStore.Services.DTOs;
using AutoMapper;

namespace GameStore.Services.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static GameDto CreateGameDto(Game entity)
        {
            GameDto dto = Mapper.Map<Game, GameDto>(entity);
            return dto;
        }

        public static Game CreateGame(GameDto dto)
        {
            Game entity = Mapper.Map<GameDto, Game>(dto);
            return entity;
        }

        public static CommentDto CreateCommentDto(Comment entity)
        {
            CommentDto dto = Mapper.Map<Comment, CommentDto>(entity);
            return dto;
        }

        public static Comment CreateComment(CommentDto dto)
        {
            Comment entity = Mapper.Map<CommentDto, Comment>(dto);
            return entity;
        }

        public static IEnumerable<Game> CreateGames(IEnumerable<GameDto> dtos)
        {
            IEnumerable<Game> entities = Mapper.Map<IEnumerable<GameDto>, IEnumerable<Game>>(dtos);
            return entities;
        }

        public static IEnumerable<GameDto> CreateGameDtos(IEnumerable<Game> entities)
        {
            IEnumerable<GameDto> dtos = Mapper.Map<IEnumerable<Game>, IEnumerable<GameDto>>(entities);
            return dtos;
        }

        public static IEnumerable<Comment> CreateComments(IEnumerable<CommentDto> dtos)
        {
            IEnumerable<Comment> entities = Mapper.Map<IEnumerable<CommentDto>, IEnumerable<Comment>>(dtos);
            return entities;
        }

        public static IEnumerable<CommentDto> CreateCommentDtos(IEnumerable<Comment> entities)
        {
            IEnumerable<CommentDto> dtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(entities);
            return dtos;
        }
    }
}
