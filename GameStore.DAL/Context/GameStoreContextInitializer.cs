using GameStore.Common.Concrete;
using GameStore.Common.Entities;
using GameStore.Common.Enums;
using System;
using System.Collections.Generic;
using GameStore.Common.Entities.Localization;
using System.Data.Entity;
using System.Linq;

namespace GameStore.DAL.Context
{
	public class GameStoreContextInitializer : CreateDatabaseIfNotExists<GameStoreContext>
	{
		private readonly Md5HashGenerator _hashGenerator = new Md5HashGenerator();

		protected override void Seed(GameStoreContext context)
		{
			SeedLanguages(context);
			SeedParentGenres(context);
			SeedGenres(context);
			SeedPlatformTypes(context);
			SeedGamesAndPublishers(context);
			SeedUsersAndRoles(context);
			SeedRandomGames(context);
			base.Seed(context);
		}

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

		private void SeedParentGenres(GameStoreContext context)
		{
			var strategy = new Genre
			{
				Name = "RTS",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Strategy",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Стратегии",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var rpg = new Genre
			{
				Name = "RPG",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "RPG",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Ролевые игры",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var races = new Genre
			{
				Name = "Races",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Races",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Гонки",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var action = new Genre
			{
				Name = "Action",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Action",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Экшен",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var adventure = new Genre
			{
				Name = "Adventure",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Adventure",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Приключения",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var puzzleAndSkill = new Genre
			{
				Name = "Puzzle&Skill",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Puzzle&Skill",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Головоломки",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var other = new Genre
			{
				Name = "Other",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Other",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Прочее",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			context.Genres.AddRange(new List<Genre> { strategy, rpg, races, action, adventure, puzzleAndSkill, other });
			context.SaveChanges();
		}

		private void SeedGenres(GameStoreContext context)
		{
			var rts = new Genre
			{
				Name = "RTS",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "RTS",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Стратегии в реальном времени",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Strategy")
			};

			var tbs = new Genre
			{
				Name = "TBS",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "TBS",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Пошаговые стратегии",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Strategy")
			};


			var rally = new Genre
			{
				Name = "Rally",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Rally",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Ралли",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Races")
			};

			var arcade = new Genre
			{
				Name = "Arcade",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Arcade",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Аркады",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Races")
			};

			var formula = new Genre
			{
				Name = "Formula",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Formula",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Формула",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Races")
			};

			var offRoad = new Genre
			{
				Name = "Off-road",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "Off-road",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Гонки по безорожью",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Races")
			};

			var fps = new Genre
			{
				Name = "FPS",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "FPS",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Шутеры от первого лица",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action")
			};

			var tps = new Genre
			{
				Name = "TPS",
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = "FPS",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GenreLocale
					{
						Name = "Шутеры от третьего лица",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},

				ParentGenre = context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action")
			};

			context.Genres.AddRange(new List<Genre> { rts, tbs, rally, arcade, formula, offRoad, fps, tps });
			context.SaveChanges();
		}

		private void SeedPlatformTypes(GameStoreContext context)
		{
			var mobile = new PlatformType
			{
				Type = "Mobile",
				PlatformTypeLocales = new List<PlatformTypeLocale>
				{
					new PlatformTypeLocale
					{
						Type = "Mobile",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new PlatformTypeLocale
					{
						Type = "Переносные устройства",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var browser = new PlatformType
			{
				Type = "Browser",
				PlatformTypeLocales = new List<PlatformTypeLocale>
				{
					new PlatformTypeLocale
					{
						Type = "Browser",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new PlatformTypeLocale
					{
						Type = "Браузеры",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var desktop = new PlatformType
			{
				Type = "Desktop",
				PlatformTypeLocales = new List<PlatformTypeLocale>
				{
					new PlatformTypeLocale
					{
						Type = "Desktop",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new PlatformTypeLocale
					{
						Type = "ПК",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			var console = new PlatformType
			{
				Type = "Console",
				PlatformTypeLocales = new List<PlatformTypeLocale>
				{
					new PlatformTypeLocale
					{
						Type = "Console",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new PlatformTypeLocale
					{
						Type = "Консоли",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				}
			};

			context.PlatformTypes.AddRange(new List<PlatformType> { mobile, browser, desktop, console });
			context.SaveChanges();
		}

		private void SeedGamesAndPublishers(GameStoreContext context)
		{
			var callOfDuty = new Game
			{
				Name = "Call Of Duty",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(1995, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "COD123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "FPS")
				},

				Publisher = new Publisher
				{
					CompanyName = "Activision",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "activision.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			var halflife = new Game
			{
				Name = "Half-Life",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(2015, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "HALF123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "FPS")
				},

				Publisher = new Publisher
				{
					CompanyName = "Valve",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "valve.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop")
				},

				Comments = new List<Comment>
				{
					new Comment
					{
						Name = "John",
						Body = "This game is good.",
						GameKey = "HALF123",
						ChildComments = new List<Comment>
						{
							new Comment
							{
								Name = "Max",
								Body = "Agree with you. This game is awesome.",
								GameKey = "HALF123",
							},

							new Comment
							{
								Name = "Helen",
								Body = "Sure thing. Played like 515161 times.",
								GameKey = "HALF123",
							}
						}
					},

					new Comment
					{
						Name = "Julia",
						Body = "This game is bad.",
						GameKey = "HALF123",
						ChildComments = new List<Comment>
						{
							new Comment
							{
								Name = "Henry",
								Body = "Julia, you are wrong. This game is awesome.",
								GameKey = "HALF123",
							}
						}
					}
				}
			};

			var dots = new Game
			{
				Name = "Dots",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(1993, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "DOTS123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Other"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Arcade")
				},

				Publisher = new Publisher
				{
					CompanyName = "Atari",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "atari.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Browser"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Mobile")
				}
			};

			var mario = new Game
			{
				Name = "Mario",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(1980, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "MARIO123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Arcade"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Adventure")
				},

				Publisher = new Publisher
				{
					CompanyName = "Nintendo",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "nintendo.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Mobile"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			var goldenAxe = new Game
			{
				Name = "Golden Axe",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(2016, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "GOLDENAXE123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Arcade"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Adventure"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Rpg")
				},

				Publisher = new Publisher
				{
					CompanyName = "Sega",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "sega.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Mobile"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			var battleToads = new Game
			{
				Name = "Dendy",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(2016, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "DENDY123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Arcade"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Adventure")
				},

				Publisher = new Publisher
				{
					CompanyName = "Dendy",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "dendy.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Mobile"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop")
				}
			};

			var fable = new Game
			{
				Name = "Fable",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(2006, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "FABLE123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Arcade"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Adventure"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "RPG"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Other")
				},

				Publisher = new Publisher
				{
					CompanyName = "Microsoft",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "microsoft.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			var diablo = new Game
			{
				Name = "Diablo",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(1999, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "DIABLO123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "RPG"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action")
				},

				Publisher = new Publisher
				{
					CompanyName = "Blizzard",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "blizzard.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			var assassinsCreed = new Game
			{
				Name = "Assassin's Creed",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(1999, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "AC123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Adventure")
				},

				Publisher = new Publisher
				{
					CompanyName = "Ubisoft",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "ubisoft.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop")
				}
			};

			var fifa15 = new Game
			{
				Name = "Fifa 15",
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(2015, 10, 14),
				DateAdded = DateTime.UtcNow,
				Key = "FIFA123",
				Price = 50,
				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Action"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Other")
				},

				Publisher = new Publisher
				{
					CompanyName = "Electronic Arts",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "ea.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Desktop"),
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			var granTurismo = new Game
			{
				Name = "Gran Turismo",
				Key = "GT123",
				Price = 50,
				Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = "The game is played from a first-person perspective and allows the player to undertake a series of assassination missions in a variety of ways, with an emphasis on player choice.",
						Language = context.Languages.First(l => l.Name == "en")
					},

					new GameLocale
					{
						Description = "Действие игры происходит в охваченном эпидемией чумы вымышленном городе Дануолл, прообразом которого послужил Лондон времён Викторианской эпохи.",
						Language = context.Languages.First(l => l.Name == "ru")
					}
				},
				DatePublished = new DateTime(2011, 10, 14),
				DateAdded = DateTime.UtcNow,

				Genres = new List<Genre>
				{
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Races"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Arcade"),
					context.Genres.First(g => g.GenreLocales.FirstOrDefault().Name == "Off-road")
				},

				Publisher = new Publisher
				{
					CompanyName = "Sony",
					PublisherLocales = new List<PublisherLocale>
					{
						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., also known as Activision, is an American video game publisher. It was founded on October 1, 1979 and was the world's first independent developer and distributor of video games for gaming consoles.",
							Language = context.Languages.First(l => l.Name == "en")
						},

						new PublisherLocale
						{
							Description = "Activision Publishing, Inc., также известная как Activision, является издателем американской видеоигры. Она была основана 1 октября 1979 года и была первым независимым разработчиком и дистрибьютором видеоигр в игровых консолях в мире",
							Language = context.Languages.First(l => l.Name == "ru")
						}
					},

					HomePage = "Sony.com"
				},

				PlatformTypes = new List<PlatformType>
				{
					context.PlatformTypes.First(p => p.PlatformTypeLocales.FirstOrDefault().Type == "Console")
				}
			};

			context.Games.AddRange(new List<Game> { halflife, callOfDuty, dots, mario, fifa15, assassinsCreed, granTurismo, battleToads, fable, goldenAxe, diablo });
			context.SaveChanges();
		}

		private void SeedLanguages(GameStoreContext context)
		{
			var en = new Language { Name = "en" };
			var ru = new Language { Name = "ru" };
			context.Languages.AddRange(new List<Language> { en, ru });
			context.SaveChanges();
		}

		private void SeedUsersAndRoles(GameStoreContext context)
		{
			var admin = new User
			{
				Login = "admin",
				Password = _hashGenerator.Generate("admin"),
				Roles = new List<Role>
				{
					new Role
					{
						Name = "Administrator",
						RoleLocales = new List<RoleLocale>
						{
							new RoleLocale
							{
								Name = "Administrator",
								Language = context.Languages.First(l => l.Name == "en")
							},

							new RoleLocale
							{
								Name = "Администратор",
								Language = context.Languages.First(l => l.Name == "ru")
							}
						},

						AccessLevel = AccessLevel.Administrator
					}
				}
			};

			var manager = new User
			{
				Login = "manager",
				Password = _hashGenerator.Generate("manager"),
				Roles = new List<Role>
				{
					new Role
					{
						Name = "Manager",
						RoleLocales = new List<RoleLocale>
						{
							new RoleLocale
							{
								Name = "Manager",
								Language = context.Languages.First(l => l.Name == "en")
							},

							new RoleLocale
							{
								Name = "Менеджер",
								Language = context.Languages.First(l => l.Name == "ru")
							}
						},

						AccessLevel = AccessLevel.Manager
					}
				}
			};

			var moderator = new User
			{
				Login = "moderator",
				Password = _hashGenerator.Generate("moderator"),
				Roles = new List<Role>
				{
					new Role
					{
						Name = "Moderator",
						RoleLocales = new List<RoleLocale>
						{
							new RoleLocale
							{
								Name = "Moderator",
								Language = context.Languages.First(l => l.Name == "en")
							},

							new RoleLocale
							{
								Name = "Модератор",
								Language = context.Languages.First(l => l.Name == "ru")
							}
						},

						AccessLevel = AccessLevel.Moderator
					}
				}
			};

			var user = new User
			{
				Login = "user",
				Password = _hashGenerator.Generate("user"),
				Roles = new List<Role>
				{
					new Role
					{
						Name = "User",
						RoleLocales = new List<RoleLocale>
						{
							new RoleLocale
							{
								Name = "User",
								Language = context.Languages.First(l => l.Name == "en")
							},

							new RoleLocale
							{
								Name = "Пользователь",
								Language = context.Languages.First(l => l.Name == "ru")
							}
						},

						AccessLevel = AccessLevel.User
					}
				}
			};

			context.Users.AddRange(new List<User> { admin, manager, moderator, user });
			context.SaveChanges();
		}
	}
}