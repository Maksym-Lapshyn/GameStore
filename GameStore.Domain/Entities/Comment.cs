using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Abstract;
using System.ComponentModel;

namespace GameStore.Domain.Entities
{
    public class Comment : ISoftDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public Game Game { get; set; }
        public Comment ParentComment { get; set; }
        public virtual ICollection<Comment> ChildComments { get; set; }
        public bool IsDeleted { get; set; }

        public Comment()
        {
            ChildComments = new List<Comment>();
        }
    }
}