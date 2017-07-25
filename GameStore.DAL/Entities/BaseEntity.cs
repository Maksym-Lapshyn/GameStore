using System;

namespace GameStore.DAL.Entities
{
	public abstract class BaseEntity
	{
		public string Id { get; set; }

		public bool IsDeleted { get; set; }
	}
}