using Common.Entities;
using PaymentService.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PaymentService.DAL.Concrete
{
	public class PaymentRepositoryStub : IPaymentRepository
	{
		private static readonly List<Payment> Transactions = new List<Payment>();

		private static int _id = 1;

		public Payment GetSingle(Expression<Func<Payment, bool>> predicate)
		{
			return Transactions.Single(predicate.Compile());
		}

		public Payment GetSingleOrDefault(Expression<Func<Payment, bool>> predicate)
		{
			return Transactions.SingleOrDefault(predicate.Compile());
		}

		public IEnumerable<Payment> GetAll(Expression<Func<Payment, bool>> predicate = null)
		{
			return predicate == null ? Transactions : Transactions.Where(predicate.Compile());
		}

		public void Insert(Payment payment)
		{
			payment.Id = _id++;
			Transactions.Add(payment);
		}

		public bool Contains(Expression<Func<Payment, bool>> predicate)
		{
			return Transactions.Any(predicate.Compile());
		}

		public void Update(Payment payment)
		{
			var index = Transactions.FindIndex(t => t.Id == payment.Id);
			Transactions[index] = payment;
		}
	}
}