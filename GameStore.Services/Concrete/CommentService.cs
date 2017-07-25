using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Services.Concrete
{
	public class CommentService : ICommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public void Create(CommentDto commentDto)
		{
			var comment = _mapper.Map<CommentDto, Comment>(commentDto);

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
			var commentDtos = _mapper.Map<IQueryable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}

		public IEnumerable<CommentDto> GetBy(string gameKey)
		{
			var comments = _unitOfWork.CommentRepository.Get().Where(c => c.Game.Key == gameKey && c.ParentComment == null);
			var commentDtos = _mapper.Map<IQueryable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}
	}
}
