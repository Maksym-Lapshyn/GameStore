using Common.Entities;
using PaymentService.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PaymentService.DAL.Concrete
{
	public class TransactionsRepository : ITransactionsRepository
	{
		private static readonly List<Transaction> Transactions = new List<Transaction>();

		public Transaction GetSingle(Expression<Func<Transaction, bool>> predicate)
		{
			return Transactions.First(predicate.Compile());
		}

		public IEnumerable<Transaction> GetAll(Expression<Func<Transaction, bool>> predicate = null)
		{
			return predicate == null ? Transactions : Transactions.Where(predicate.Compile());
		}

		public void Insert(Transaction transaction)
		{
			Transactions.Add(transaction);
		}

		public void Delete(int transactionId)
		{
			Transactions.First(t => t.Id == transactionId).IsDeleted = true;
		}

		public bool Contains(Expression<Func<Transaction, bool>> predicate)
		{
			return Transactions.Any(predicate.Compile());
		}
	}
}