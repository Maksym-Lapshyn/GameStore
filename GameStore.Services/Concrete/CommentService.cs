using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.DAL.Entities;

namespace GameStore.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(CommentDto commentDto)
        {
            var comment = Mapper.Map<CommentDto, Comment>(commentDto);
            if (comment.ParentCommentId != null)
            {
                comment.ParentComment = comment;
            }

            else
            {
                var game = _unitOfWork.GameRepository.Get().First(g => g.Id == commentDto.GameId);
                comment.Game = game;
            }

            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.Save();
        }

        public void Edit(CommentDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CommentDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDto> GetAll()
        {
            var comments = _unitOfWork.CommentRepository.Get();
            var commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

            return commentDtos;
        }

        public IEnumerable<CommentDto> GetBy(string gameKey)
        {
            var game = _unitOfWork.GameRepository.Get().First(g => String.Equals(g.Key, gameKey, StringComparison.CurrentCultureIgnoreCase));
            var comments = game.Comments.Where(c => c.ParentComment == null);
            var commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

            return commentDtos;
        }
    }
}
