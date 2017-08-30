using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using System.Data.Entity;

namespace GameStore.DAL.Context
{
	public class GameStoreContext : DbContext
	{
		static GameStoreContext()
		{
			Database.SetInitializer(new GameStoreContextInitializer());
		}

		public GameStoreContext(string connectionString)
		: base(connectionString)
		{
			Configuration.ProxyCreationEnabled = true;
		}

		public DbSet<Game> Games { get; set; }

		public DbSet<GameLocale> GameLocales { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<GenreLocale> GenreLocales { get; set; }

		public DbSet<PlatformType> PlatformTypes { get; set; }

		public DbSet<PlatformTypeLocale> PlatformTypeLocales { get; set; }

		public DbSet<Publisher> Publishers { get; set; }

		public DbSet<PublisherLocale> PublisherLocales { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderDetails> OrderDetails { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<RoleLocale> RoleLocales { get; set; }

		public DbSet<Language> Languages { get; set; }
	}
}