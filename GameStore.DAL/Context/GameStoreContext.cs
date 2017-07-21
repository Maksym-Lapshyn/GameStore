using GameStore.DAL.Entities;
using System.Data.Entity;

namespace GameStore.DAL.Context
{
	public class GameStoreContext : DbContext
	{
		public GameStoreContext(string connectionString)
		: base(connectionString)
		{
		}

		public DbSet<Game> Games { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<PlatformType> PlatformTypes { get; set; }

		public DbSet<Publisher> Publishers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetails> OrderDetails { get; set; }

		static GameStoreContext()
		{
			Database.SetInitializer(new GameStoreContextInitializer());
		}
	}
}