using AutoMapper;
using GameStore.Common.Entities;
using GameStore.DAL.Abstract.Common;
using GameStore.Services.Abstract;
using GameStore.Services.Dtos;
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
			var game = _gameRepository.GetSingle(g => g.Key == comment.GameKey);

			if (comment.ParentCommentId != null)
			{
				comment.ParentComment = _commentRepository.GetSingle(c => c.Id == comment.ParentCommentId.Value);
			}

			game.Comments.Add(comment);

			_gameRepository.Update(game);

			_unitOfWork.Save();
		}

		public IEnumerable<CommentDto> GetAll()
		{
			var comments = _commentRepository.GetAll();
			var commentDtos = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}

		public IEnumerable<CommentDto> GetAll(string gameKey)
		{
			var comments = _commentRepository.GetAll().Where(c => c.GameKey == gameKey && c.ParentCommentId == null);
			var commentDtos = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(comments);

			return commentDtos;
		}

		public CommentDto GetSingle(int id)
		{
			var comment = _commentRepository.GetSingle(c => c.Id == id);

			return _mapper.Map<Comment, CommentDto>(comment);
		}

		public CommentDto GetSingleOrDefault(int id)
		{
			var comment = _commentRepository.GetSingleOrDefault(c => c.Id == id);

			if (comment == null)
			{
				return null;
			}

			return _mapper.Map<Comment, CommentDto>(comment);
		}

		public void Delete(int id)
		{
			_commentRepository.Delete(id);

			_unitOfWork.Save();
		}

		public bool Contains(string gameKey)
		{
			return _commentRepository.Contains(c => c.GameKey == gameKey);
		}

		public bool Contains(int id)
		{
			return _commentRepository.Contains(c => c.Id == id);
		}

		public void Update(CommentDto commentDto)
		{
			var comment = _commentRepository.GetSingle(c => c.Id == commentDto.Id);
			comment = _mapper.Map(commentDto, comment);

			_commentRepository.Update(comment);

			_unitOfWork.Save();
		}
	}
}