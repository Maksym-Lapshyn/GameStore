using System.Collections.Generic;
using System.Linq;
using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.DAL.Abstract.Localization;
using GameStore.Services.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class GameInputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<ILanguageRepository> _mockOfLanguageRepository;
		private GameInputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfLanguageRepository = new Mock<ILanguageRepository>();
			_mockOfLanguageRepository.Setup(m => m.GetSingleBy(It.IsAny<string>()))
				.Returns(new Language {Name = ValidString});
			_target = new GameInputLocalizer(_mockOfLanguageRepository.Object);
		}

		[TestMethod]
		public void Localize_AddsNewGameLocale_WhenGameDoesNotContainLocales()
		{
			var game = new Game
			{
				GameLocales = new List<GameLocale>()
			};

			_target.Localize(InvalidString, game);

			Assert.AreEqual(1, game.GameLocales.Count);
		}

		[TestMethod]
		public void Localize_AddsNewGameLocale_WhenGameDoesNotContainCurrentLocale()
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

			Assert.AreEqual(2, game.GameLocales.Count);
		}

		[TestMethod]
		public void Localize_ChangesGameLocaleDescription_WhenGameContainsCurrentLocale()
		{
			var game = new Game
			{
				Description =  ValidString,

				GameLocales = new List<GameLocale>
				{
					new GameLocale
					{
						Description = InvalidString,

						Language = new Language
						{
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, game);

			Assert.AreEqual(ValidString, game.GameLocales.First().Description);
		}
	}
}