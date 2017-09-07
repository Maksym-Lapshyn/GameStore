using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PaymentService.DAL.Abstract
{
	public interface IAccountRepository
	{
		Account GetSingle(Expression<Func<Account, bool>> predicate);

		Account GetSingleOrDefault(Expression<Func<Account, bool>> predicate);

		IEnumerable<Account> GetAll(Expression<Func<Account, bool>> predicate = null);

		void Insert(Account account);

		bool Contains(Expression<Func<Account, bool>> predicate);

		void Update(Account account);
	}
}