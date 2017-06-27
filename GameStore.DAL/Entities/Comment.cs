﻿using System.Collections.Generic;
using GameStore.DAL.Abstract;

namespace GameStore.DAL.Entities
{
	public class Comment : BaseEntity
    {
        public string Name { get; set; }

        public string Body { get; set; }

        public int? GameId { get; set; }

        public virtual Game Game { get; set; }

        public int? ParentCommentId { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual ICollection<Comment> ChildComments { get; set; }

        public Comment()
        {
            ChildComments = new List<Comment>();
        }
    }
}