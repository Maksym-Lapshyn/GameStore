using System.Collections.Generic;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface ICommentService : IService<CommentDto>
    {
        IEnumerable<CommentDto> GetBy(string gameKey);
	}
}
