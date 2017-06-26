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

        public void AddCommentToGame(CommentDto newComment)
        {
            Game game = _unitOfWork.GameRepository.Get().FirstOrDefault(g => g.Id == newComment.GameId);
            if (game != null)
            {
                Comment comment = Mapper.Map<CommentDto, Comment>(newComment);
                comment.Game = game;
                _unitOfWork.CommentRepository.Insert(comment);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("There is no existing game for adding this comment");
            }
        }

        public void AddCommentToComment(CommentDto newComment)
        {
            Comment oldComment =
                _unitOfWork.CommentRepository.Get().FirstOrDefault(c => c.Id == newComment.ParentCommentId);
            if (oldComment != null)
            {
                Comment comment = Mapper.Map<CommentDto, Comment>(newComment);
                comment.ParentComment = oldComment;
                _unitOfWork.CommentRepository.Insert(comment);
                _unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("There is no existing comment for adding a new one");
            }
        }

        public IEnumerable<CommentDto> GetAllCommentsByGameKey(string key)
        {
            Game game = _unitOfWork.GameRepository.Get().FirstOrDefault(g => g.Key.ToLower() == key.ToLower());
            if (game != null)
            {
                IEnumerable<Comment> comments = game.Comments;
                if (comments != null)
                {
                    IEnumerable<CommentDto> commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);
                    return commentDtos;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException("There is no game with such key");
            }
        }
    }
}
