using AutoMapper;
using GameStore.DAL.Abstract.Common;
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
		private readonly ICommentRepository _commentRepository;
		private readonly IGameRepository _gameRepository;

		public CommentService(IUnitOfWork unitOfWork, 
			IMapper mapper,
			ICommentRepository commentRepository,
			IGameRepository gameRepository)
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
				parentComment.ChildComments.Add(comment);
				_commentRepository.Update(parentComment);
			}
			else
			{
				var game = _gameRepository.Get(comment.GameKey);
				game.Comments.Add(comment);
				_gameRepository.Update(game);
			}
			
			_unitOfWork.Save();
		}

		public IEnumerable<CommentDto> GetAll()
		{
			var comments = _commentRepository.Get();
			var commentDtos = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}

		public IEnumerable<CommentDto> GetBy(string gameKey)
		{
			var comments = _commentRepository.Get().Where(c => c.GameKey == gameKey && c.ParentCommentId == null);
			var commentDtos = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}
	}
}