using GameStore.Common.Entities;
using GameStore.DAL.Abstract;
using System;

namespace GameStore.DAL.Concrete
{
	public class GameLogContainer : ILogContainer<Game>
	{
		public DateTime DateChanged { get; set; }

		public string Action { get; set; }

		public string EntityType { get; set; }

		public Game Old { get; set; }

		public Game New { get; set; }
	}
}