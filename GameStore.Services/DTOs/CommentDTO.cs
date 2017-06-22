using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Services.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public GameDTO Game { get; set; }
        public CommentDTO ParentComment { get; set; }
        public virtual ICollection<CommentDTO> ChildComments { get; set; }
        public bool IsDeleted { get; set; }

        public CommentDTO()
        {
            ChildComments = new List<CommentDTO>();
        }
    }
}
