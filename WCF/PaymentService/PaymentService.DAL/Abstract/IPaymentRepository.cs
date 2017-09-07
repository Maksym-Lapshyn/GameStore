using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PaymentService.DAL.Abstract
{
	public interface IPaymentRepository
	{
		Payment GetSingle(Expression<Func<Payment, bool>> predicate);

		Payment GetSingleOrDefault(Expression<Func<Payment, bool>> predicate);

		IEnumerable<Payment> GetAll(Expression<Func<Payment, bool>> predicate = null);

		void Insert(Payment payment);

		bool Contains(Expression<Func<Payment, bool>> predicate);

		void Update(Payment payment);
	}
}