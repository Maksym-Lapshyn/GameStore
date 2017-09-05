using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PaymentService.DAL.Abstract
{
	public interface IAccountsRepository
	{
		Account GetSingle(Expression<Func<Account, bool>> predicate);

		IEnumerable<Account> GetAll(Expression<Func<Account, bool>> predicate = null);

		void Insert(Account account);

		void Delete(int accountId);

		bool Contains(Expression<Func<Account, bool>> predicate);
	}
}