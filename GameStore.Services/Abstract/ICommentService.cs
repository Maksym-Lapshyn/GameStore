using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	public interface ICommentService : IService<CommentDto>
    {
		void Add(CommentDto comment);

        IEnumerable<CommentDto> GetBy(string gameKey);
	}
}
