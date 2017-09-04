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
	public class RoleInputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<ILanguageRepository> _mockOfLanguageRepository;
		private RoleInputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfLanguageRepository = new Mock<ILanguageRepository>();
			_mockOfLanguageRepository.Setup(m => m.GetSingleBy(It.IsAny<string>()))
				.Returns(new Language {Name = ValidString});
			_target = new RoleInputLocalizer(_mockOfLanguageRepository.Object);
		}

		[TestMethod]
		public void Localize_AddsNewRoleLocale_WhenGameDoesNotContainLocales()
		{
			var role = new Role
			{
				RoleLocales = new List<RoleLocale>()
			};

			_target.Localize(InvalidString, role);

			Assert.AreEqual(1, role.RoleLocales.Count);
		}

		[TestMethod]
		public void Localize_AddsNewRoleLocale_WhenGameDoesNotContainCurrentLocale()
		{
			var role = new Role
			{
				RoleLocales = new List<RoleLocale>
				{
					new RoleLocale
					{
						Name = ValidString,

						Language = new Language
						{
							Name = InvalidString
						}
					}
				}
			};

			_target.Localize(ValidString, role);

			Assert.AreEqual(2, role.RoleLocales.Count);
		}

		[TestMethod]
		public void Localize_ChangesRoleLocaleDescription_WhenGameContainsCurrentLocale()
		{
			var role = new Role
			{
				Name =  ValidString,

				RoleLocales = new List<RoleLocale>
				{
					new RoleLocale
					{
						Name = InvalidString,

						Language = new Language
						{
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, role);

			Assert.AreEqual(ValidString, role.RoleLocales.First().Name);
		}
	}
}