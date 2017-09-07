using Common.Abstract;
using Common.Entities;
using PaymentService.Application.Dtos;
using PaymentService.DAL.Abstract;
using System;
using PaymentService.Application.Infrastructure.Abstract;

namespace PaymentService.Application
{
	public class PaymentService : IPaymentService
	{
		private const string FirstDigitOfVisaCardNumber = "4";
		private const string FirstDigitOfMasterCardNumber = "5";

		private readonly IConfirmationMessageSender _messageSender;
		private readonly IConfirmationEmailSender _emailSender;
		private readonly IPaymentRepository _paymentRepository;
		private readonly IAccountRepository _accountRepository;
		private readonly ILogger _logger;

		public PaymentService(IConfirmationMessageSender messageSender,
			IConfirmationEmailSender emailSender,
			IPaymentRepository paymentRepository,
			IAccountRepository accountRepository,
			ILogger logger)
		{
			_messageSender = messageSender;
			_emailSender = emailSender;
			_paymentRepository = paymentRepository;
			_accountRepository = accountRepository;
			_logger = logger;
		}

		public PaymentResponse ConductPurchase(Payment payment)
		{
			payment.PaymentStatus = PaymentStatus.Pending;

			CheckIfTransactionIsAllowed(payment);

			var confirmationCode = GenerateConfirmationCode();

			if (payment.Phone != null && payment.PaymentStatus == PaymentStatus.Pending)
			{
				_messageSender.Send(confirmationCode);

				payment.ConfirmationCode = confirmationCode;
			}
			else if (payment.Email != null && payment.PaymentStatus == PaymentStatus.Pending)
			{
				_emailSender.Send(confirmationCode);

				payment.ConfirmationCode = confirmationCode;
			}

			if ((payment.Phone == null || payment.Email == null) && payment.PaymentStatus == PaymentStatus.Pending)
			{
				payment.PaymentStatus = PaymentStatus.Successful;

				ProcessPayment(payment);
			}

			_paymentRepository.Insert(payment);
			LogTransaction(payment);

			var response = new PaymentResponse { PaymentStatus = payment.PaymentStatus, PaymentId = payment.Id };

			return response;
		}

		public PaymentResponse ConfirmPayment(int paymentId, string confirmationCode)
		{
			PaymentStatus paymentStatus;

			var payment = _paymentRepository.GetSingleOrDefault(t => t.Id == paymentId);

			if (payment == null)
			{
				paymentStatus = PaymentStatus.TransactionIdIsNotCorrect;
			}
			else if (payment.ConfirmationCode != confirmationCode)
			{
				paymentStatus = PaymentStatus.Pending;
			}
			else
			{
				payment.PaymentStatus = PaymentStatus.Successful;
				paymentStatus = PaymentStatus.Successful;
			}

			LogTransaction(payment);

			var response = new PaymentResponse { PaymentStatus = paymentStatus, PaymentId = paymentId };

			return response;
		}

		private void LogTransaction(Payment payment)
		{
			var message = $"Id: {payment.Id}. Status: {payment.PaymentStatus}. Supplier: {payment.BuyersCardNumber}. Consumer: {payment.SellersCardNumber}. Transaction amount: {payment.PaymentAmount}. Transaction date: {DateTime.UtcNow}";
			_logger.LogPayment(message);
		}

		private string GenerateConfirmationCode()
		{
			var randomizer = new Random();
			var number = randomizer.Next(10000000, 99999999);

			return number.ToString();
		}

		private void CheckIfTransactionIsAllowed(Payment payment)
		{
			if (!_accountRepository.Contains(a => a.CardNumber == payment.SellersCardNumber) ||
				!_accountRepository.Contains(a => a.CardNumber == payment.BuyersCardNumber))
			{
				payment.PaymentStatus = PaymentStatus.CardDoesNotExist;
			}

			var buyersAccount = _accountRepository.GetSingle(a => a.CardNumber == payment.BuyersCardNumber);
			var names = payment.BuyersFullName.Split(' ');

			string firstName;
			string lastName;

			try
			{
				firstName = names[0];
				lastName = names[1];
			}
			catch (ArgumentOutOfRangeException)
			{
				firstName = default(string);
				lastName = default(string);
			}

			if (buyersAccount.CvvCode != payment.CvvCode
				|| buyersAccount.ExpirationDate.Year != payment.ExpirationYear
				|| buyersAccount.ExpirationDate.Month != payment.ExpirationMonth
				|| buyersAccount.Owner.FirstName != firstName
				|| buyersAccount.Owner.LastName != lastName)
			{
				payment.PaymentStatus = PaymentStatus.PaymentFailed;
			}

			if (buyersAccount.Balance < payment.PaymentAmount)
			{
				payment.PaymentStatus = PaymentStatus.NotEnoughMoney;
			}
		}

		private bool CheckIfCardIsVisa(string buyersCardNumber)
		{
			return buyersCardNumber.StartsWith(FirstDigitOfVisaCardNumber);
		}

		private bool CheckIfCardIsMasterCard(string buyersCardNumber)
		{
			return buyersCardNumber.StartsWith(FirstDigitOfMasterCardNumber);
		}

		private void ProcessVisaPayment(string sellersCardNumber, string buyersCardNumber, decimal paymentAmount)
		{
			var sellersAccount = _accountRepository.GetSingle(a => a.CardNumber == buyersCardNumber);
			var buyersAccount = _accountRepository.GetSingle(a => a.CardNumber == sellersCardNumber);
			sellersAccount.Balance -= paymentAmount;
			buyersAccount.Balance += paymentAmount;
		}

		private void ProcessMasterCardPayment(string sellersCardNumber, string buyersCardNumber, decimal paymentAmount)
		{
			var sellersAccount = _accountRepository.GetSingle(a => a.CardNumber == buyersCardNumber);
			var buyersAccount = _accountRepository.GetSingle(a => a.CardNumber == sellersCardNumber);
			sellersAccount.Balance -= paymentAmount;
			buyersAccount.Balance += paymentAmount;
		}

		private void ProcessPayment(Payment payment)
		{
			if (CheckIfCardIsVisa(payment.SellersCardNumber))
			{
				ProcessVisaPayment(payment.SellersCardNumber, payment.BuyersCardNumber, payment.PaymentAmount);
			}
			else if (CheckIfCardIsMasterCard(payment.BuyersCardNumber))
			{
				ProcessMasterCardPayment(payment.SellersCardNumber, payment.BuyersCardNumber, payment.PaymentAmount);
			}
			else
			{
				payment.PaymentStatus = PaymentStatus.NotSupportedCard;
			}
		}
	}
}