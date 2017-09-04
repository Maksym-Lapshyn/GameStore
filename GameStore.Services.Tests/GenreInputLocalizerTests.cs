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
	public class GenreInputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<ILanguageRepository> _mockOfLanguageRepository;
		private GenreInputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfLanguageRepository = new Mock<ILanguageRepository>();
			_mockOfLanguageRepository.Setup(m => m.GetSingleBy(It.IsAny<string>()))
				.Returns(new Language {Name = ValidString});
			_target = new GenreInputLocalizer(_mockOfLanguageRepository.Object);
		}

		[TestMethod]
		public void Localize_AddsNewGenreLocale_WhenGameDoesNotContainLocales()
		{
			var genre = new Genre
			{
				GenreLocales = new List<GenreLocale>()
			};

			_target.Localize(InvalidString, genre);

			Assert.AreEqual(1, genre.GenreLocales.Count);
		}

		[TestMethod]
		public void Localize_AddsNewGenreLocale_WhenGameDoesNotContainCurrentLocale()
		{
			var genre = new Genre
			{
				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = ValidString,

						Language = new Language
						{
							Name = InvalidString
						}
					}
				}
			};

			_target.Localize(ValidString, genre);

			Assert.AreEqual(2, genre.GenreLocales.Count);
		}

		[TestMethod]
		public void Localize_ChangesGenreLocaleDescription_WhenGameContainsCurrentLocale()
		{
			var genre = new Genre
			{
				Name =  ValidString,

				GenreLocales = new List<GenreLocale>
				{
					new GenreLocale
					{
						Name = InvalidString,

						Language = new Language
						{
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, genre);

			Assert.AreEqual(ValidString, genre.GenreLocales.First().Name);
		}
	}
}