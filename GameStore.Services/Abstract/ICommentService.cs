using GameStore.Services.Dtos;
using System.Collections.Generic;

namespace GameStore.Services.Abstract
{
	public interface ICommentService
	{
		IEnumerable<CommentDto> GetAll(string gameKey);

		bool Contains(string gameKey);

		bool Contains(int id);

		void Create(CommentDto entity);

		IEnumerable<CommentDto> GetAll();

		CommentDto GetSingle(int id);

		void Update(CommentDto commentDto);

		void Delete(int id);
	}
}