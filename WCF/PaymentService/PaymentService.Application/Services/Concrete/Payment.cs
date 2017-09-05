using PaymentService.Application.Dtos;
using PaymentService.Application.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentService.Application.Services.Concrete
{
	public class Payment : IPayment
	{
		private const int FirstDayOfMonth = 1;
		private const string FirstDigitOfVisaCardNumber = "4";
		private const string FirstDigitOfMasterCardNumber = "5";

		private readonly List<Account> _accounts = AccountsRepository.Accounts;
		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private static readonly List<Tuple<Guid, Transaction, int>> PendingTransactions = new List<Tuple<Guid, Transaction, int>>();

		public TransactionResponse ConductPurchase(Transaction transaction)
		{
			var paymentStatus = CheckIfTransactionIsAllowed(transaction);
			var transactionId = Guid.NewGuid();

			if (transaction.Phone != null && paymentStatus == PaymentStatus.Pending)
			{
				PendingTransactions.Add(new Tuple<Guid, Transaction, int>(transactionId, transaction, GenerateConfirmationCode()));
			}
			else
			{
				if (CheckIfCardIsVisa(transaction.ConsumerCardNumber))
				{
					ProcessVisaPayment(transaction.ConsumerCardNumber, transaction.SupplierCardNumber, transaction.PaymentAmount);
				}
				else if (CheckIfCardIsMasterCard(transaction.SupplierCardNumber))
				{
					ProcessMasterCardPayment(transaction.ConsumerCardNumber, transaction.SupplierCardNumber, transaction.PaymentAmount);
				}

				paymentStatus = PaymentStatus.Successful;
			}

			LogTransaction(transactionId, paymentStatus, transaction);
			var response = new TransactionResponse { PaymentStatus = paymentStatus, TransactionId = transactionId };

			return response;
		}

		public TransactionResponse ConfirmTransaction(Guid transactionId, int confirmationCode)
		{
			var paymentStatus = default(PaymentStatus);
			var tuple = PendingTransactions.FirstOrDefault(t => t.Item1 == transactionId);

			if (tuple == default(Tuple<Guid, Transaction, int>))
			{
				
			}

			if (PendingTransactions.All(t => t.Item1 != transactionId))
			{
				paymentStatus = PaymentStatus.PaymentFailed;
			}
			else if (PendingTransactions.First(t => t.Item1 == transactionId).Item3 != confirmationCode)
			{
				paymentStatus = PaymentStatus.ConfirmationCodeIsNotCorrect;
			}
			else
			{
				paymentStatus = PaymentStatus.Successful;
			}

			var response = new TransactionResponse { PaymentStatus = paymentStatus, TransactionId = transactionId };

			return response;
		}

		private void LogTransaction(Guid transactionId, PaymentStatus paymentStatus, Transaction transaction)
		{
			var message = $"Id: {transactionId}. Status: {paymentStatus} Supplier: {transaction.SupplierCardNumber}. Consumer: {transaction.ConsumerCardNumber}. Transaction amount: {transaction.PaymentAmount}. Transaction date: {DateTime.UtcNow}";
			_logger.Info(message);
		}

		private int GenerateConfirmationCode()
		{
			var randomizer = new Random();
			var number = randomizer.Next(10000000, 99999999);

			return number;
		}

		private PaymentStatus CheckIfTransactionIsAllowed(Transaction transaction)
		{
			var paymentStatus = default(PaymentStatus);

			if (_accounts.Any(a => a.CardNumber == transaction.ConsumerCardNumber) ||
				_accounts.Any(a => a.CardNumber == transaction.SupplierCardNumber))
			{
				paymentStatus = PaymentStatus.CardDoesNotExist;
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
				paymentStatus = PaymentStatus.PaymentFailed;
			}

			if (supplierAccount.Balance < transaction.PaymentAmount)
			{
				paymentStatus = PaymentStatus.NotEnoughMoney;
			}

			return paymentStatus == default(PaymentStatus) ? PaymentStatus.Pending : paymentStatus;
		}

		private bool CheckIfCardIsVisa(long consumerCardNumber)
		{
			return consumerCardNumber.ToString().StartsWith(FirstDigitOfVisaCardNumber);
		}

		private bool CheckIfCardIsMasterCard(long consumerCardNumber)
		{
			return consumerCardNumber.ToString().StartsWith(FirstDigitOfMasterCardNumber);
		}

		private void ProcessVisaPayment(long supplierCardNumber, long consumerCardNumber, decimal paymentAmount)
		{
			var consumerAccount = _accounts.First(a => a.CardNumber == consumerCardNumber);
			var supplierAccount = _accounts.First(a => a.CardNumber == supplierCardNumber);
			consumerAccount.Balance -= paymentAmount;
			supplierAccount.Balance += paymentAmount;
		}

		private void ProcessMasterCardPayment(long supplierCardNumber, long consumerCardNumber, decimal paymentAmount)
		{
			var consumerAccount = _accounts.First(a => a.CardNumber == consumerCardNumber);
			var supplierAccount = _accounts.First(a => a.CardNumber == supplierCardNumber);
			consumerAccount.Balance -= paymentAmount;
			supplierAccount.Balance += paymentAmount;
		}
	}
}