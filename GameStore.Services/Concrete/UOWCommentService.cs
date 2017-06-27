using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameStore.Services.Infrastructure;
using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using GameStore.DAL.Entities;

namespace GameStore.Services.Concrete
{
    //TODO: Required: Remove 'Uow' prefix Fixed in ML_2
    public class UowCommentService : ICommentService
    {
        //TODO: Consider: make fields readonly Fixed in ML_2
        private IUnitOfWork _unitOfWork;

        public UowCommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(CommentDto entity)
        {
            throw new NotImplementedException();
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
            IEnumerable<Comment> comments = _unitOfWork.CommentRepository.Get();
            IEnumerable<CommentDto> commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

            return commentDtos;
        }

		public void Add(CommentDto commentDto)
        {
            Comment comment = Mapper.Map<CommentDto, Comment>(commentDto);
            if (comment.ParentCommentId != null)
            {
                comment.ParentComment = comment;
            }
            else
            {
                Game game = _unitOfWork.GameRepository.Get().First(g => g.Id == commentDto.GameId);
                comment.Game = game;
            }
            _unitOfWork.CommentRepository.Insert(comment);
            _unitOfWork.Save();
        }

        public IEnumerable<CommentDto> GetBy(string gameKey)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => String.Equals(g.Key, gameKey, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Comment> comments = game.Comments;
            IEnumerable<CommentDto> commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

            return commentDtos;
        }
    }
}
