﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DAL.Entities
{
	public class Game : BaseEntity
	{
		[StringLength(450)]
		[Index(IsUnique = true)]
		public string Key { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		[Column(TypeName = "MONEY")]
		public decimal Price { get; set; }

		public DateTime DateAdded { get; set; }

		public DateTime DatePublished { get; set; }

		public int ViewsCount { get; set; }

		public short UnitsInStock { get; set; }

		public bool Discontinued { get; set; }

		public int? PublisherId { get; set; }

		public virtual Publisher Publisher { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<Genre> Genres { get; set; }

		public virtual ICollection<PlatformType> PlatformTypes { get; set; }
	}
}
