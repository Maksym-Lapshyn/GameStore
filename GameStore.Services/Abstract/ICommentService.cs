using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Services.DTOs;

namespace GameStore.Services.Abstract
{
    public interface ICommentService : IService<CommentDTO>
    {
        void AddCommentToGame(string gameKey, CommentDTO comment);
        void AddCommentToComment(CommentDTO comment);
        IEnumerable<CommentDTO> GetAllCommentsByGameKey(string key);
    }
}
