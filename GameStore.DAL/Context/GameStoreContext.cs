using System.Data.Entity;
using GameStore.Common.Entities;

namespace GameStore.DAL.Context
{
	public class GameStoreContext : DbContext
	{
		public GameStoreContext(string connectionString)
		: base(connectionString)
		{
			Configuration.ProxyCreationEnabled = true;
		}

		public DbSet<Game> Games { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<PlatformType> PlatformTypes { get; set; }

		public DbSet<Publisher> Publishers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetails> OrderDetails { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		static GameStoreContext()
		{
			Database.SetInitializer(new GameStoreContextInitializer());
		}
	}
}