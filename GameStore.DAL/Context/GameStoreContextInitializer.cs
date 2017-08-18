using GameStore.Common.Concrete;
using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Context
{
	public class GameStoreContextInitializer : CreateDatabaseIfNotExists<GameStoreContext>
	{
		private readonly Md5Hasher _hasher = new Md5Hasher();

		private void SeedRandomGames(GameStoreContext context)
		{
			var genres = context.Genres.ToList();
			var platforms = context.PlatformTypes.ToList();
			var publishers = context.Publishers.ToList();
			var rnd = new Random();

			for (var i = 0; i < 89; i++)
			{
				var game = new Game
				{
					Name = $"game number {i}",
					Key = $"gamekey{i}",
					Price = 50,
					Description = $"game description{i}",
					DateAdded = DateTime.UtcNow,
					DatePublished = DateTime.UtcNow,
					Publisher = publishers[rnd.Next(0, publishers.Count - 1)],
					PlatformTypes = new List<PlatformType>
					{
						platforms[rnd.Next(0, platforms.Count - 1)]
					},

					Genres = new List<Genre>
					{
						genres[rnd.Next(0, genres.Count - 1)]
					}
				};

				context.Games.Add(game);
			}

			context.SaveChanges();
		}
		protected override void Seed(GameStoreContext context)
		{
			var rts = new Genre { Name = "RTS" };
			var tbs = new Genre { Name = "TBS" };
			var strategy = new Genre { Name = "Strategy" };
			var rpg = new Genre { Name = "RPG" };
			var races = new Genre { Name = "Races" };
			var action = new Genre { Name = "Action" };
			var adventure = new Genre { Name = "Adventure" };
			var puzzleAndSkill = new Genre { Name = "Puzzle&Skill" };
			var misc = new Genre { Name = "Misc" };
			var rally = new Genre { Name = "Rally", ParentGenre = races };
			var arcade = new Genre { Name = "Arcade", ParentGenre = races };
			var formula = new Genre { Name = "Formula", ParentGenre = races };
			var offRoad = new Genre { Name = "Off-road", ParentGenre = races };
			var fps = new Genre { Name = "FPS", ParentGenre = strategy};
			var tps = new Genre { Name = "TPS", ParentGenre = strategy };
			var subMisc = new Genre { Name = "Misc(Sub-genre)", ParentGenre = strategy };
			var other = new Genre {Name = "Other"};
			context.Genres.AddRange(new List<Genre> { rts, tbs, rally, arcade, formula, offRoad, fps, tps, subMisc, strategy, rpg, adventure, puzzleAndSkill, misc, other });
			context.SaveChanges();
			var mobile = new PlatformType { Type = "Mobile" };
			var browser = new PlatformType { Type = "Browser" };
			var desktop = new PlatformType { Type = "Desktop" };
			var console = new PlatformType { Type = "Console" };
			var callOfDuty = new Game
			{
				Name = "Call Of Duty",
				Description = "You can shoot some enemies",
				DatePublished = new DateTime(1995, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "COD123",
				Price = 50,
				Genres = new List<Genre>
				{
					action, fps
				},

				Publisher = new Publisher
				{
					CompanyName = "Activision",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "activision.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					desktop, console
				}
			};

			var halflife = new Game
			{
				Name = "Half-Life",
				Description = "You can shoot some enemies v2",
				DatePublished = new DateTime(2015, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "HALF123",
				Price = 50,
				Genres = new List<Genre>
				{
					action, fps
				},

				Publisher = new Publisher
				{
					CompanyName = "Valve",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "valve.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					desktop
				}
			};

			var dots = new Game
			{
				Name = "Dots",
				Description = "You can shoot some enemies v3",
				DatePublished = new DateTime(1993, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "DOTS123",
				Price = 50,
				Genres = new List<Genre>
				{
					misc, arcade, puzzleAndSkill
				},

				Publisher = new Publisher
				{
					CompanyName = "Atari",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "atari.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					browser, mobile
				}
			};

			var mario = new Game
			{
				Name = "Mario",
				Description = "You can shoot some enemies v4",
				DatePublished = new DateTime(1980, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "MARIO123",
				Price = 50,
				Genres = new List<Genre>
				{
					arcade, adventure
				},

				Publisher = new Publisher
				{
					CompanyName = "Nintendo",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "nintendo.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					mobile, console
				}
			};

			var goldenAxe = new Game
			{
				Name = "Golden Axe",
				Description = "You can shoot some enemies v5",
				DatePublished = new DateTime(2016, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "GOLDENAXE123",
				Price = 50,
				Genres = new List<Genre>
				{
					arcade, adventure, rpg
				},

				Publisher = new Publisher
				{
					CompanyName = "Sega",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "sega.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					mobile, console, desktop
				}
			};

			var battleToads = new Game
			{
				Name = "Dendy",
				Description = "You can shoot some enemies v6",
				DatePublished = new DateTime(2016, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "DENDY123",
				Price = 50,
				Genres = new List<Genre>
				{
					arcade, adventure
				},

				Publisher = new Publisher
				{
					CompanyName = "Dendy",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "dendy.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					mobile, console, desktop
				}
			};

			var fable = new Game
			{
				Name = "Fable",
				Description = "You can shoot some enemies v7",
				DatePublished = new DateTime(2006, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "FABLE123",
				Price = 50,
				Genres = new List<Genre>
				{
					arcade, adventure, rpg, misc
				},

				Publisher = new Publisher
				{
					CompanyName = "Microsoft",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "microsoft.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					console
				}
			};

			var diablo = new Game
			{
				Name = "Diablo",
				Description = "You can shoot some enemies v8",
				DatePublished = new DateTime(1999, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "DIABLO123",
				Price = 50,
				Genres = new List<Genre>
				{
					rpg, action
				},

				Publisher = new Publisher
				{
					CompanyName = "Blizzard",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "blizzard.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					console, desktop
				}
			};

			var assassinsCreed = new Game
			{
				Name = "Assassin's Creed",
				Description = "You can shoot some enemies v9",
				DatePublished = new DateTime(1999, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "AC123",
				Price = 50,
				Genres = new List<Genre>
				{
					action, adventure
				},

				Publisher = new Publisher
				{
					CompanyName = "Ubisoft",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "ubisoft.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					console, desktop
				}
			};

			var fifa15 = new Game
			{
				Name = "Fifa 15",
				Description = "You can shoot some enemies v10",
				DatePublished = new DateTime(2015, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "FIFA123",
				Price = 50,
				Genres = new List<Genre>
				{
					action, misc
				},

				Publisher = new Publisher
				{
					CompanyName = "Electronic Arts",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "ea.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					console, desktop
				}
			};

			var granTurismo = new Game
			{
				Name = "Gran Turismo",
				Key = "GT123",
				Price = 50,
				Description = "You can shoot some enemies v10",
				DatePublished = new DateTime(2011, 10, 14),
				DateAdded = DateTime.UtcNow,

				Genres = new List<Genre>
				{
					races, arcade, offRoad
				},

				Publisher = new Publisher
				{
					CompanyName = "Sony",
					Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
					HomePage = "Sony.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					console
				}
			};

			context.Games.AddRange(new List<Game> { halflife, callOfDuty, dots, mario, fifa15, assassinsCreed, granTurismo, battleToads, fable, goldenAxe, diablo});
			context.SaveChanges();

			var admin = new User
			{
				Login = "admin",
				Password = _hasher.GenerateHash("admin"),
				Roles = new List<Role>
				{
					new Role {Name = "Administrator", AccessLevel = AccessLevel.Administrator}
				}
			};

			var manager = new User
			{
				Login = "manager",
				Password = _hasher.GenerateHash("manager"),
				Roles = new List<Role>
				{
					new Role { Name = "Manager", AccessLevel = AccessLevel.Manager }
				}
			};

			var moderator = new User
			{
				Login = "moderator",
				Password = _hasher.GenerateHash("moderator"),
				Roles = new List<Role>
				{
					new Role { Name = "Moderator", AccessLevel = AccessLevel.Moderator }
				}
			};

			var user = new User
			{
				Login = "user",
				Password = _hasher.GenerateHash("user"),
				Roles = new List<Role>
				{
					new Role { Name = "User", AccessLevel = AccessLevel.User }
				}
			};
;
			context.Users.AddRange(new List<User> {admin, manager, moderator, user});
			context.SaveChanges();
			SeedRandomGames(context);
			base.Seed(context);
		}
	}
}