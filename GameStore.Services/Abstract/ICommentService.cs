using System.Collections.Generic;
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
