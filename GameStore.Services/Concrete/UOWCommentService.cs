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
    public class UowCommentService : ICommentService
    {
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
            var commentDtos = AutoMapperFactory.CreateCommentDtos(comments);
            return commentDtos;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AddCommentToGame(CommentDto newComment)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Id == newComment.GameId);
            Comment comment = AutoMapperFactory.CreateComment(newComment);
            game.Comments.Add(comment);
            _unitOfWork.Save();
        }

        public void AddCommentToComment(CommentDto newComment)
        {
            Comment oldComment = _unitOfWork.CommentRepository.GetById(newComment.ParentCommentId);
            Comment comment = AutoMapperFactory.CreateComment(newComment);
            oldComment.ChildComments.Add(comment);
            _unitOfWork.Save();
        }

        public IEnumerable<CommentDto> GetAllCommentsByGameKey(string key)
        {
            IEnumerable<Comment> comments = _unitOfWork.GameRepository.Get().First(g => g.Key.ToLower() == key.ToLower()).Comments;
            IEnumerable<CommentDto> commentDtos = AutoMapperFactory.CreateCommentDtos(comments);
            return commentDtos;
        }
    }
}
