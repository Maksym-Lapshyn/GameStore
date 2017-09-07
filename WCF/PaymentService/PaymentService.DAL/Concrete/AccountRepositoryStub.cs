using Common.Entities;
using PaymentService.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PaymentService.DAL.Concrete
{
	public class AccountRepositoryStub : IAccountRepository
	{
		private static readonly List<Account> Accounts = new List<Account>();

		private static int _id = 1;

		static AccountRepositoryStub()
		{
			Initialize();
		}

		public Account GetSingle(Expression<Func<Account, bool>> predicate)
		{
			return Accounts.Single(predicate.Compile());
		}

		public Account GetSingleOrDefault(Expression<Func<Account, bool>> predicate)
		{
			return Accounts.SingleOrDefault(predicate.Compile());
		}

		public IEnumerable<Account> GetAll(Expression<Func<Account, bool>> predicate = null)
		{
			return predicate == null ? Accounts : Accounts.Where(predicate.Compile());
		}

		public void Insert(Account account)
		{
			account.Id = ++_id;
			Accounts.Add(account);
		}

		public bool Contains(Expression<Func<Account, bool>> predicate)
		{
			return Accounts.Any(predicate.Compile());
		}

		public void Update(Account account)
		{
			var index = Accounts.FindIndex(a => a.Id == account.Id);
			Accounts[index] = account;
		}

		private static void Initialize()
		{
			var visa = new Account
			{
				Id = _id++,
				Balance = 500,
				CardNumber = "4024007160378997",
				CvvCode = 123,
				ExpirationDate = DateTime.UtcNow.AddDays(5),
				Owner = new User
				{
					Email = "johndoe@gmail.com",
					FirstName = "John",
					LastName = "Doe"
				}
			};

			var masterCard = new Account
			{
				Id = _id++,
				Balance = 1000,
				CardNumber = "5401323513514067",
				CvvCode = 321,
				ExpirationDate = DateTime.UtcNow.AddDays(5),
				Owner = new User
				{
					Email = "gamestore@gmail.com",
					FirstName = "Game",
					LastName = "Store"
				}
			};

			Accounts.AddRange(new List<Account> { visa, masterCard });
		}
	}
}