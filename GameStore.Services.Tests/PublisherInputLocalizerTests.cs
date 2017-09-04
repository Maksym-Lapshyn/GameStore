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
	public class PublisherInputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<ILanguageRepository> _mockOfLanguageRepository;
		private PublisherInputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfLanguageRepository = new Mock<ILanguageRepository>();
			_mockOfLanguageRepository.Setup(m => m.GetSingleBy(It.IsAny<string>()))
				.Returns(new Language {Name = ValidString});
			_target = new PublisherInputLocalizer(_mockOfLanguageRepository.Object);
		}

		[TestMethod]
		public void Localize_AddsNewPublisherLocale_WhenGameDoesNotContainLocales()
		{
			var publisher = new Publisher
			{
				PublisherLocales = new List<PublisherLocale>()
			};

			_target.Localize(InvalidString, publisher);

			Assert.AreEqual(1, publisher.PublisherLocales.Count);
		}

		[TestMethod]
		public void Localize_AddsNewPublisherLocale_WhenGameDoesNotContainCurrentLocale()
		{
			var publisher = new Publisher
			{
				PublisherLocales = new List<PublisherLocale>
				{
					new PublisherLocale
					{
						Description = ValidString,

						Language = new Language
						{
							Name = InvalidString
						}
					}
				}
			};

			_target.Localize(ValidString, publisher);

			Assert.AreEqual(2, publisher.PublisherLocales.Count);
		}

		[TestMethod]
		public void Localize_ChangesPublisherLocaleDescription_WhenGameContainsCurrentLocale()
		{
			var publisher = new Publisher
			{
				Description =  ValidString,

				PublisherLocales = new List<PublisherLocale>
				{
					new PublisherLocale
					{
						Description = InvalidString,

						Language = new Language
						{
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, publisher);

			Assert.AreEqual(ValidString, publisher.PublisherLocales.First().Description);
		}
	}
}