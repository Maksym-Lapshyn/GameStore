using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.Services.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class PlatformTypeOutputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private PlatformTypeOutputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_target = new PlatformTypeOutputLocalizer();
		}

		[TestMethod]
		public void Localize_DoesNotChangePlatformTypeTypeAndPlatformTypeLocales_WhenPlatformTypeLocalesAreEmpty()
		{
			var platformType = new PlatformType
			{
				Type = ValidString,
				PlatformTypeLocales = new List<PlatformTypeLocale>()
			};

			_target.Localize(ValidString, platformType);

			Assert.AreEqual(0, platformType.PlatformTypeLocales.Count);
			Assert.AreEqual(ValidString, platformType.Type);
		}

		[TestMethod]
		public void Localize_ChangesPlatformTypeType_WhenPlatformTypeContainsCurrentLocale()
		{
			var platformType = new PlatformType
			{
				PlatformTypeLocales = new List<PlatformTypeLocale>
				{
					new PlatformTypeLocale
					{
						Type = ValidString,

						Language = new Language
						{
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, platformType);

			Assert.AreEqual(ValidString, platformType.Type);
		}

		[TestMethod]
		public void Localize_ChangesPlatformTypeType_WhenPlatformTypeDoesNotContainCurrentLocale()
		{
			var platformType = new PlatformType
			{
				PlatformTypeLocales = new List<PlatformTypeLocale>
				{
					new PlatformTypeLocale
					{
						Type = ValidString,

						Language = new Language
						{
							Name = InvalidString
						}
					}
				}
			};

			_target.Localize(ValidString, platformType);

			Assert.AreEqual(ValidString, platformType.Type);
		}
	}
}