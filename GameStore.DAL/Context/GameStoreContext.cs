namespace GameStore.DAL.Context
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using GameStore.Domain.Entities;

    public class GameStoreContext : DbContext
    {
        // Your context has been configured to use a 'GameStoreContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'GameStore.DAL.Context.GameStoreContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'GameStoreContext' 
        // connection string in the application configuration file.
        public GameStoreContext()
            : base("name=GameStoreContext")
        {
        }

        static GameStoreContext()
        {
            System.Data.Entity.Database.SetInitializer<GameStoreContext>(new GameStoreContextInitializer());
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}