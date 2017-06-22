using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.Domain.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;

namespace GameStore.Services.Concrete
{
    public class UOWCommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;

        public UOWCommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(CommentDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(CommentDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public CommentDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            IEnumerable<Comment> comments = _unitOfWork.CommentRepository.Get();
            var commentDTOs = Mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(comments);
            return commentDTOs;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AddCommentToGame(string gameKey, CommentDTO newComment)
        {
            Game game = _unitOfWork.GameRepository.Get().First(g => g.Key == gameKey);
            Comment comment = Mapper.Map<CommentDTO, Comment>(newComment);
            game.Comments.Add(comment);
            _unitOfWork.Save();
        }

        public void AddCommentToComment(CommentDTO newComment)
        {
            Comment comment = Mapper.Map<CommentDTO, Comment>(newComment);
            Comment oldComment = _unitOfWork.CommentRepository.GetById(newComment.ParentComment.Id);
            oldComment.ChildComments.Add(comment);
            _unitOfWork.Save();
        }

        public IEnumerable<CommentDTO> GetAllCommentsByGameKey(string key)
        {
            IEnumerable<Comment> comments = _unitOfWork.GameRepository.Get().First(g => g.Key == key).Comments;
            IEnumerable<CommentDTO> commentDTOs = Mapper.Map<IEnumerable<Comment>, List<CommentDTO>>(comments);
            return commentDTOs;
        }


    }
}
