using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PaymentService.DAL.Abstract
{
	public interface ITransactionsRepository
	{
		Transaction GetSingle(Expression<Func<Transaction, bool>> predicate);

		IEnumerable<Transaction> GetAll(Expression<Func<Transaction, bool>> predicate = null);

		void Insert(Transaction transaction);

		void Delete(int transactionId);

		bool Contains(Expression<Func<Transaction, bool>> predicate);
	}
}