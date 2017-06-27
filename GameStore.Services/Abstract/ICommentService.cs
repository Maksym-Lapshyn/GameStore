using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface ICommentService : IService<CommentDto>
    {
		void Add(CommentDto comment);

        IEnumerable<CommentDto> GetBy(string gameKey);
	}
}
