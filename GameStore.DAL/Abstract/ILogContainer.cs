using System;

namespace GameStore.DAL.Abstract
{
	public interface ILogContainer<T>
	{
		DateTime DateChanged { get; set; }

		string Action { get; set; }

		string EntityType { get; set; }

		T Old { get; set; }

		T New { get; set; }
	}
}