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
        public Comment Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public bool IsDeleted { get; set; }

        public Comment()
        {
            Comments = new List<Comment>();
        }
    }
}