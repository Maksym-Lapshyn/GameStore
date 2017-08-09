using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface ICommentService : IService
	{
		IEnumerable<CommentDto> GetBy(string gameKey);

		void Create(CommentDto entity);

		IEnumerable<CommentDto> GetAll();
	}
}