using AutoMapper;
using GameStore.DAL.Abstract;
using GameStore.DAL.Entities;
using GameStore.Services.Abstract;
using GameStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using GameStore.DAL.Abstract.EntityFramework;

namespace GameStore.Services.Concrete
{
	public class CommentService : ICommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IEfCommentRepository _commentRepository;
		private readonly IEfGameRepository _gameRepository;

		public CommentService(IUnitOfWork unitOfWork, 
			IMapper mapper,
			IEfCommentRepository commentRepository,
			IEfGameRepository gameRepository)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_commentRepository = commentRepository;
			_gameRepository = gameRepository;
		}

		public void Create(CommentDto commentDto)
		{
			var comment = _mapper.Map<CommentDto, Comment>(commentDto);

			if (comment.ParentCommentId != null)
			{
				var parentComment = _commentRepository.Get(comment.ParentCommentId.Value);
				comment.ParentComment = parentComment;
			}

			var game = _gameRepository.Get(comment.GameKey);
			comment.Game = game;
			_commentRepository.Insert(comment);
			_unitOfWork.Save();
		}

		public IEnumerable<CommentDto> GetAll()
		{
			var comments = _commentRepository.Get();
			var commentDtos = _mapper.Map<IQueryable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}

		public IEnumerable<CommentDto> GetBy(string gameKey)
		{
			var comments = _commentRepository.Get().Where(c => c.Game.Key == gameKey && c.ParentComment == null);
			var commentDtos = _mapper.Map<IQueryable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}
	}
}
