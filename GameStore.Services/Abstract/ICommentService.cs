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
        void AddCommentToGame(CommentDto comment);
        void AddCommentToComment(CommentDto comment);
        IEnumerable<CommentDto> GetAllCommentsByGameKey(string key);
    }
}
