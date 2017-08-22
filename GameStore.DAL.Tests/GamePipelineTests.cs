using GameStore.Common.Entities;
using GameStore.Common.Enums;
using GameStore.DAL.Abstract;
using GameStore.DAL.Concrete;
using GameStore.DAL.Concrete.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.DAL.Tests
{
	[TestClass]
	public class GamePipelineTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";
		private const int ValidInt = 10;
		private readonly IPipeline<IQueryable<Game>> _target = new GamePipeline();
		private IQueryable<Game> _games;

		[TestMethod]
		public void GamePipeline_FiltersByDateOptions_WhenLastWeekIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-5000)
				},

				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-5)
				}
			}.AsQueryable();

			_target.Register(new DateOptionsFilter(DateOptions.LastMonth));
			var dateForComparison = DateTime.UtcNow.AddDays(-7);

			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.DatePublished > dateForComparison));
		}

		[TestMethod]
		public void GamePipeline_FiltersByDateOptions_WhenLastMonthIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-5000)
				},

				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-10)
				}
			}.AsQueryable();

			_target.Register(new DateOptionsFilter(DateOptions.LastMonth));
			var dateForComparison = DateTime.UtcNow.AddDays(-30);

			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.DatePublished > dateForComparison));
		}

		[TestMethod]
		public void GamePipeline_FiltersByDateOptions_WhenLastYearIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-5000)
				},

				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-50)
				}
			}.AsQueryable();

			_target.Register(new DateOptionsFilter(DateOptions.LastYear));
			var dateForComparison = DateTime.UtcNow.AddDays(-365);

			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.DatePublished > dateForComparison));
		}

		[TestMethod]
		public void GamePipeline_FiltersByDateOptions_WhenTwoYearsAreSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-5000)
				},

				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-500)
				}
			}.AsQueryable();

			_target.Register(new DateOptionsFilter(DateOptions.LastYear));
			var dateForComparison = DateTime.UtcNow.AddDays(-730);

			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.DatePublished > dateForComparison));
		}

		[TestMethod]
		public void GamePipeline_FiltersByDateOptions_WhenThreeYearsAreSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-5000)
				},

				new Game
				{
					DatePublished = DateTime.UtcNow.AddDays(-800)
				}
			}.AsQueryable();

			_target.Register(new DateOptionsFilter(DateOptions.LastYear));
			var dateForComparison = DateTime.UtcNow.AddDays(-1095);

			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.DatePublished > dateForComparison));
		}

		[TestMethod]
		public void GamePipeline_FiltersByGameName_WhenValidNameIsPassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					Name = "insanegamecooltest"
				},

				new Game
				{
					Name = "two"
				},

				new Game
				{
					Name = "sanetestgadsh"
				}
			}.AsQueryable();

			_target.Register(new GameNameFilter(ValidString));
			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.Name.Contains(ValidString)));
		}

		[TestMethod]
		public void GamePipeline_FiltersByGameName_WhenInvalidNameIsPassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					Name = "insanegamecooltest"
				},

				new Game
				{
					Name = "two"
				},

				new Game
				{
					Name = "sanetestgadsh"
				}
			}.AsQueryable();

			_target.Register(new GameNameFilter(InvalidString));
			var result = _target.Process(_games).Count();

			Assert.AreEqual(result, 0);
		}

		[TestMethod]
		public void GamePipeline_FiltersByGenre_WhenGenreIdsArePassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					Genres = new List<Genre>
					{
						new Genre{Name = ValidString}
					}
				},

				new Game
				{
					Genres = new List<Genre>
					{
						new Genre{Name=ValidString}
					}
				},

				new Game
				{
					Genres = new List<Genre>
					{
						new Genre{Name=InvalidString}
					}
				}
			}.AsQueryable();

			var genres = new List<string> { ValidString };

			_target.Register(new GenreFilter(genres));
			var result = _target.Process(_games).Count();

			Assert.AreEqual(result, 2);
		}

		[TestMethod]
		public void GamePipeline_FiltersByMinPrice_WhenValidPriceIsPassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					Price = 20
				},

				new Game
				{
					Price = 30
				},

				new Game
				{
					Price = 5
				}
			}.AsQueryable();

			_target.Register(new MinPriceFilter(ValidInt));
			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.Price > ValidInt));
		}

		[TestMethod]
		public void GamePipeline_FiltersByMaxPrice_WhenValidPriceIsPassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					Price = 20
				},

				new Game
				{
					Price = 30
				},

				new Game
				{
					Price = 5
				}
			}.AsQueryable();

			_target.Register(new MaxPriceFilter(ValidInt));
			var result = _target.Process(_games);

			Assert.IsTrue(result.All(game => game.Price < ValidInt));
		}

		[TestMethod]
		public void GamePipeline_FiltersByPlatformType_WhenPlatformTypesArePassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					PlatformTypes = new List<PlatformType>
					{
						new PlatformType{Type=ValidString}
					}
				},

				new Game
				{
					PlatformTypes = new List<PlatformType>
					{
						new PlatformType{Type=ValidString}
					}
				},

				new Game
				{
					PlatformTypes = new List<PlatformType>
					{
						new PlatformType{Type=InvalidString}
					}
				}
			}.AsQueryable();

			var platformTypes = new List<string> { ValidString };

			_target.Register(new PlatformTypeFilter(platformTypes));
			var result = _target.Process(_games).Count();

			Assert.AreEqual(result, 2);
		}

		[TestMethod]
		public void GamePipeline_FiltersByPublisher_WhenCompanyNamesArePassed()
		{
			_games = new List<Game>
			{
				new Game
				{
					Publisher = new Publisher{CompanyName = ValidString}
				},

				new Game
				{
					Publisher = new Publisher{CompanyName = ValidString}
				},

				new Game
				{
					Publisher = new Publisher{CompanyName = InvalidString}
				}
			}.AsQueryable();

			var companyNames = new List<string> { ValidString };

			_target.Register(new PublisherFilter(companyNames));
			var result = _target.Process(_games).Count();

			Assert.AreEqual(result, 2);
		}

		[TestMethod]
		public void GamePipeline_OrdersByPriceAscending_WhenPriceAscendingIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					Price = 30
				},

				new Game
				{
					Price = 10
				},

				new Game
				{
					Price = 20
				}
			}.AsQueryable();

			_target.Register(new SortOptionsFilter(SortOptions.PriceAscending));
			var result = _target.Process(_games).ToList();

			Assert.AreEqual(result[0].Price, 10);
			Assert.AreEqual(result[1].Price, 20);
			Assert.AreEqual(result[2].Price, 30);
		}

		[TestMethod]
		public void GamePipeline_OrdersByPriceDescending_WhenPriceDescendingIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					Price = 30
				},

				new Game
				{
					Price = 10
				},

				new Game
				{
					Price = 20
				}
			}.AsQueryable();

			_target.Register(new SortOptionsFilter(SortOptions.PriceDescending));
			var result = _target.Process(_games).ToList();

			Assert.AreEqual(result[0].Price, 30);
			Assert.AreEqual(result[1].Price, 20);
			Assert.AreEqual(result[2].Price, 10);
		}

		[TestMethod]
		public void GamePipeline_OrdersByDateAdded_WhenDateAddedIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					Id = 1,
					DateAdded = DateTime.UtcNow.AddDays(-50)
				},

				new Game
				{
					Id = 2,
					DateAdded = DateTime.UtcNow.AddDays(-25)
				},

				new Game
				{
					Id = 3,
					DateAdded = DateTime.UtcNow.AddDays(-75)
				}
			}.AsQueryable();

			_target.Register(new SortOptionsFilter(SortOptions.DateAdded));
			var result = _target.Process(_games).ToList();

			Assert.AreEqual(result[0].Id, 3);
			Assert.AreEqual(result[1].Id, 1);
			Assert.AreEqual(result[2].Id, 2);
		}

		[TestMethod]
		public void GamePipeline_OrdersByMostCommented_WhenMostCommentedIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					Id = 1,
					Comments = new List<Comment>
					{
						new Comment(),
						new Comment(),
						new Comment()
					}
				},

				new Game
				{
					Id = 2,
					Comments = new List<Comment>
					{
						new Comment()
					}
				},

				new Game
				{
					Id = 3,
					Comments = new List<Comment>
					{
						new Comment(),
						new Comment()
					}
				},
			}.AsQueryable();

			_target.Register(new SortOptionsFilter(SortOptions.MostCommented));
			var result = _target.Process(_games).ToList();

			Assert.AreEqual(result[0].Id, 1);
			Assert.AreEqual(result[1].Id, 3);
			Assert.AreEqual(result[2].Id, 2);
		}

		[TestMethod]
		public void GamePipeline_OrdersByMostViewed_WhenMostViewedIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					Id = 1,
					ViewsCount = 3
				},

				new Game
				{
					Id = 2,
					ViewsCount = 1
				},

				new Game
				{
					Id = 3,
					ViewsCount = 2
				},
			}.AsQueryable();

			_target.Register(new SortOptionsFilter(SortOptions.MostViewed));
			var result = _target.Process(_games).ToList();

			Assert.AreEqual(result[0].Id, 1);
			Assert.AreEqual(result[1].Id, 3);
			Assert.AreEqual(result[2].Id, 2);
		}

		[TestMethod]
		public void GamePipeline_OrdersByDateAdded_WhenNoneIsSelected()
		{
			_games = new List<Game>
			{
				new Game
				{
					Id = 1,
					DateAdded = DateTime.UtcNow.AddDays(-50)
				},

				new Game
				{
					Id = 2,
					DateAdded = DateTime.UtcNow.AddDays(-25)
				},

				new Game
				{
					Id = 3,
					DateAdded = DateTime.UtcNow.AddDays(-75)
				}
			}.AsQueryable();

			_target.Register(new SortOptionsFilter(SortOptions.DateAdded));
			var result = _target.Process(_games).ToList();

			Assert.AreEqual(result[0].Id, 3);
			Assert.AreEqual(result[1].Id, 1);
			Assert.AreEqual(result[2].Id, 2);
		}
	}
}