using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface ICommentService
	{
		IEnumerable<CommentDto> GetAll(string gameKey);

		void Create(CommentDto entity);

		IEnumerable<CommentDto> GetAll();

		CommentDto GetSingle(int id);

		void Update(CommentDto commentDto);
	}
}