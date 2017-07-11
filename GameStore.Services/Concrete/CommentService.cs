using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;

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
				var parentComment = _unitOfWork.CommentRepository.Get(comment.ParentCommentId.Value);
				comment.ParentComment = parentComment;
			}

			var game = _unitOfWork.GameRepository.Get(comment.Id);
			comment.Game = game;
			_unitOfWork.CommentRepository.Insert(comment);
			_unitOfWork.Save();
		}

		public IEnumerable<CommentDto> GetAll()
		{
			var comments = _unitOfWork.CommentRepository.Get();
			var commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}

		public IEnumerable<CommentDto> GetBy(string gameKey)
		{
			var comments = _unitOfWork.CommentRepository.Get(c => c.Game.Key == gameKey && c.ParentComment == null);
			var commentDtos = Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}
	}
}
