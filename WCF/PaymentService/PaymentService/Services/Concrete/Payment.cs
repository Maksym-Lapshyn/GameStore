using Common.Entities;
using PaymentService.Dtos;
using PaymentService.Repositories;
using PaymentService.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentService.Services.Concrete
{
	public class Payment : IPayment
	{
		private const int FirstDayOfMonth = 1;

		private readonly List<Account> _accounts = AccountsRepository.Accounts;

		private static List<Guid> _pendingTransactions = new List<Guid>();

		public TransactionResponse ConductPurchase(Transaction transaction)
		{
			if (_accounts.Any(a => a.CardNumber == transaction.ConsumerCardNumber) ||
				_accounts.Any(a => a.CardNumber == transaction.SupplierCardNumber))
			{
				return new TransactionResponse { PaymentStatus = PaymentStatus.CardDoesNotExist, TransactionId = default(int) };
			}

			var supplierAccount = _accounts.First(a => a.CardNumber == transaction.SupplierCardNumber);
			var expirationDate = new DateTime(transaction.ExpirationYear, transaction.ExpirationMonth, FirstDayOfMonth);
			var names = transaction.SupplierFullName.Split(' ');
			var firstName = names[0];
			var lastName = names[1];

			if (supplierAccount.CvvCode != transaction.CvvCode
				|| supplierAccount.ExpirationDate < expirationDate
				|| supplierAccount.Owner.FirstName != firstName
				|| supplierAccount.Owner.LastName != lastName)
			{
				return new TransactionResponse { PaymentStatus = PaymentStatus.PaymentFailed, TransactionId = default(Guid) };
			}

			if (supplierAccount.Balance < transaction.PaymentAmount)
			{
				return new TransactionResponse { PaymentStatus = PaymentStatus.NotEnoughMoney, TransactionId = default(Guid) };
			}

			if (transaction.Phone != null)
			{
				return new TransactionResponse { PaymentStatus = PaymentStatus.Pending, TransactionId = Guid.NewGuid() };
			}
		}

		public TransactionResponse ConfirmTransaction(int transactionId, int confirmationCode)
		{
			throw new NotImplementedException();
		}

		private void ProcessVisaPayment(long consumerCardNumber, long supplierCardNumber, decimal transactionAmount)
		{
			var consumerAccount = _accounts.First(a => a.CardNumber == consumerCardNumber);
			var supplierAccount = _accounts.First(a => a.CardNumber == supplierCardNumber);
			consumerAccount.Balance += transactionAmount;
			supplierAccount.Balance -= transactionAmount;

		}

		private void ProcessMasterCardPayment(long consumerCardNumber, long supplierCardNumber, decimal transactionAmount)
		{

		}
	}
}