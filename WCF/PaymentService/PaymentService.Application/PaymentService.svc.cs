using Common.Abstract;
using Common.Entities;
using PaymentService.Application.Dtos;
using PaymentService.Application.Infrastructure.Abstract;
using PaymentService.DAL.Abstract;
using System;

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
			payment.PaymentStatus = CheckIfTransactionIsAllowed(payment);
			var confirmationCode = GenerateConfirmationCode();

			if (payment.PaymentStatus == PaymentStatus.Pending)
			{
				if (payment.Phone != null)
				{
					_messageSender.SendPhone(payment.Phone, confirmationCode);

					payment.ConfirmationCode = confirmationCode;
				}
				else if (payment.Email != null)
				{
					_emailSender.SendEmail(payment.Email, confirmationCode);

					payment.ConfirmationCode = confirmationCode;
				}

				if (payment.Phone == null && payment.Email == null)
				{
					payment.PaymentStatus = PaymentStatus.Successful;

					ProcessPayment(payment);
				}
			}
			
			_paymentRepository.Insert(payment);
			LogPayment(payment);

			return new PaymentResponse
			{
				PaymentStatus = payment.PaymentStatus,
				PaymentId = payment.Id
			};
		}

		public PaymentResponse ConfirmPayment(int paymentId, string confirmationCode)
		{
			var payment = _paymentRepository.GetSingleOrDefault(t => t.Id == paymentId);

			if (payment == null)
			{
				return new PaymentResponse
				{
					PaymentStatus = PaymentStatus.TransactionIdIsNotCorrect,
					PaymentId = paymentId
				};
			}

			payment.PaymentStatus = payment.ConfirmationCode != confirmationCode 
				? PaymentStatus.Pending 
				: PaymentStatus.Successful;

			_paymentRepository.Update(payment);
			LogPayment(payment);

			return new PaymentResponse
			{
				PaymentStatus = payment.PaymentStatus,
				PaymentId = paymentId
			};
		}

		private PaymentStatus CheckIfTransactionIsAllowed(Payment payment)
		{
			if (!_accountRepository.Contains(a => a.CardNumber == payment.SellersCardNumber) ||
				!_accountRepository.Contains(a => a.CardNumber == payment.BuyersCardNumber))
			{
				return PaymentStatus.CardDoesNotExist;
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
			catch (IndexOutOfRangeException)
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
				return PaymentStatus.PaymentFailed;
			}

			if (buyersAccount.Balance < payment.PaymentAmount)
			{
				return PaymentStatus.NotEnoughMoney;
			}

			return PaymentStatus.Pending;
		}

		private void LogPayment(Payment payment)
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