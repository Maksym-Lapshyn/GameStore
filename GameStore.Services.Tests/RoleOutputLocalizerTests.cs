using GameStore.Common.Entities;
using GameStore.Common.Entities.Localization;
using GameStore.Services.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GameStore.Services.Tests
{
	[TestClass]
	public class RoleOutputLocalizerTests
	{
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private RoleOutputLocalizer _target;

		[TestInitialize]
		public void Initialize()
		{
			_target = new RoleOutputLocalizer();
		}

		[TestMethod]
		public void Localize_DoesNotChangeRoleNameAndRoleLocales_WhenRoleLocalesAreEmpty()
		{
			var role = new Role
			{
				Name = ValidString,
				RoleLocales = new List<RoleLocale>()
			};

			_target.Localize(ValidString, role);

			Assert.AreEqual(0, role.RoleLocales.Count);
			Assert.AreEqual(ValidString, role.Name);
		}

		[TestMethod]
		public void Localize_ChangesRoleName_WhenRoleContainsCurrentLocale()
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
							Name = ValidString
						}
					}
				}
			};

			_target.Localize(ValidString, role);

			Assert.AreEqual(ValidString, role.Name);
		}

		[TestMethod]
		public void Localize_ChangesRoleName_WhenRoleDoesNotContainCurrentLocale()
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

			Assert.AreEqual(ValidString, role.Name);
		}
	}
}