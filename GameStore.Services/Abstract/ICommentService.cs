using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
	//TODO: Required: Blank line after each method/property
	public interface ICommentService : IService<CommentDto>
    {
		//TODO: Required: Rename to 'Add' and join logic of AddCommentToGame and AddCommentToComment
		void AddCommentToGame(CommentDto comment);
        void AddCommentToComment(CommentDto comment);
        IEnumerable<CommentDto> GetAllCommentsByGameKey(string key);  //TODO: Consider: Rename to 'GetBy(string gameKey)'
	}
}
