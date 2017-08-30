using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.Services.Abstract;
using GameStore.Services.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class GameOutputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<IOutputLocalizer<Genre>> _mockOfGenreOutputLocalizer;
		private Mock<IOutputLocalizer<PlatformType>> _mockOfPlatformTypeOutputLocalizer;
		private Mock<IOutputLocalizer<Publisher>> _mockOfPublisherOutputLocalizer;
		private GameOutputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfGenreOutputLocalizer = new Mock<IOutputLocalizer<Genre>>();
			_mockOfPlatformTypeOutputLocalizer = new Mock<IOutputLocalizer<PlatformType>>();
			_mockOfPublisherOutputLocalizer = new Mock<IOutputLocalizer<Publisher>>();
			_target = new GameOutputLocalizer(_mockOfGenreOutputLocalizer.Object, _mockOfPlatformTypeOutputLocalizer.Object, _mockOfPublisherOutputLocalizer.Object);
		}

		[TestMethod]
		public void Localize_DoesNotChangeGameDescriptionAndGameLocales_WhenGameLocalesAreEmpty()
		{
			var game = new Game
			{
				Description = ValidString,
				GameLocales = new List<GameLocale>()
			};

			_target.Localize(ValidString, game);

			Assert.AreEqual(0, game.GameLocales.Count);
			Assert.AreEqual(ValidString, game.Description);
		}

		[TestMethod]
		public void Localize_ChangesGameDescription_WhenGameContainsCurrentLocale()
		{
			var game = new Game
			{
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = ValidString,

						Language = new Language
						{
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, game);

			Assert.AreEqual(ValidString, game.Description);
		}

		[TestMethod]
		public void Localize_ChangesGameDescription_WhenGameDoesNotContainCurrentLocale()
		{
			var game = new Game
			{
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = ValidString,

						Language = new Language
						{
							Name = InvalidString
						}
					}
				}
			};

			_target.Localize(ValidString, game);

			Assert.AreEqual(ValidString, game.Description);
		}

		[TestMethod]
		public void Localize_LocalizesGenres_WhenGameContainsGenres()
		{
			var game = new Game
			{
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Language = new Language
						{
							Name = InvalidString
						}
					}
				},

				Genres = new List<Genre>
				{
					new Genre()
				}
			};

			_target.Localize(ValidString, game);

			_mockOfGenreOutputLocalizer.Verify(m => m.Localize(ValidString, It.IsAny<Genre>()), Times.Once);
		}

		[TestMethod]
		public void Localize_LocalizesPublisher_WhenGameContainsPublisher()
		{
			var game = new Game
			{
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Language = new Language
						{
							Name = InvalidString
						}
					}
				},

				Publisher = new Publisher()
			};

			_target.Localize(ValidString, game);

			_mockOfPublisherOutputLocalizer.Verify(m => m.Localize(ValidString, It.IsAny<Publisher>()), Times.Once);
		}

		[TestMethod]
		public void Localize_LocalizesPlatformTypes_WhenGameContainsPlatformTypes()
		{
			var game = new Game
			{
				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Language = new Language
						{
							Name = InvalidString
						}
					}
				},

				PlatformTypes = new List<PlatformType>
				{
					new PlatformType()
				}
			};

			_target.Localize(ValidString, game);

			_mockOfPlatformTypeOutputLocalizer.Verify(m => m.Localize(ValidString, It.IsAny<PlatformType>()), Times.Once);
		}
	}
}