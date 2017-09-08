using Common.Abstract;
using Common.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PaymentService.Application.Infrastructure.Abstract;
using PaymentService.DAL.Abstract;
using System;
using System.Linq.Expressions;


namespace PaymentService.Application.Tests
{
	[TestClass]
	public class PaymentServiceTests
	{
		private const string InvalidCardNumber = "test";
		private const string ValidCardNumber = "4444555544445555";
		private const string InvalidFullName = "test";
		private const int InvalidInt = 0;
		private const int ValidInt = 10;
		private const string ValidFullName = "John Doe";
		private const string FirstName = "John";
		private const string LastName = "Doe";
		private const string ValidString = "test";
		private const string InvalidString = "testtest";

		private Mock<IConfirmationMessageSender> _mockOfMessageSender;
		private Mock<IConfirmationEmailSender> _mockOfEmailSender;
		private Mock<IAccountRepository> _mockOfAccountRepository;
		private Mock<IPaymentRepository> _mockOfPaymentRepository;
		private Mock<ILogger> _mockOfLogger;
		private PaymentService _target;
		private Payment _payment;
		private Account _account;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfMessageSender = new Mock<IConfirmationMessageSender>();
			_mockOfEmailSender = new Mock<IConfirmationEmailSender>();
			_mockOfAccountRepository = new Mock<IAccountRepository>();
			_mockOfPaymentRepository = new Mock<IPaymentRepository>();
			_mockOfLogger = new Mock<ILogger>();
			_target = new PaymentService(_mockOfMessageSender.Object, _mockOfEmailSender.Object, _mockOfPaymentRepository.Object, _mockOfAccountRepository.Object, _mockOfLogger.Object);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusCardDoesNotExist_WhenInvalidCardNumbersArePassed()
		{
			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(false);
			_payment = new Payment
			{
				BuyersCardNumber = InvalidCardNumber,
				SellersCardNumber = InvalidCardNumber
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.CardDoesNotExist, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusPaymentFailed_WhenInvalidExpirationMonthIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				ExpirationMonth = InvalidInt,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.PaymentFailed, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusPaymentFailed_WhenInvalidExpirationYearIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				ExpirationMonth = DateTime.UtcNow.Year,
				ExpirationYear = InvalidInt,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.PaymentFailed, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusPaymentFailed_WhenInvalidFullNameIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = InvalidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.PaymentFailed, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusNotEnoughMoney_WhenInvalidFullNameIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Balance = InvalidInt,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.NotEnoughMoney, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusSuccessfull_WhenValidParametersWithoutPhoneAndEmailArePassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Balance = ValidInt,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.Successful, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusPending_WhenValidParametersWithPhoneArePassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Balance = ValidInt,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				Phone = ValidString,
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.Pending, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_CallsSendPhoneOnce_WhenValidParametersWithPhoneArePassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Balance = ValidInt,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				Phone = ValidString,
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			_target.ConductPurchase(_payment);

			_mockOfMessageSender.Verify(m => m.SendPhone(ValidString, It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusPending_WhenValidParametersWithEmailArePassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Balance = ValidInt,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				Email = ValidString,
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			var response = _target.ConductPurchase(_payment);

			Assert.AreEqual(PaymentStatus.Pending, response.PaymentStatus);
		}

		[TestMethod]
		public void ConductPurchase_CallsSendEmailOnce_WhenValidParametersWithPhoneArePassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Balance = ValidInt,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				Email = ValidString,
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			_target.ConductPurchase(_payment);

			_mockOfEmailSender.Verify(m => m.SendEmail(ValidString, It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void ConductPurchase_CallsInsertOnce_WhenPaymentWithInvalidPropertiesIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				ExpirationMonth = InvalidInt,
				ExpirationYear = InvalidInt,
				BuyersCardNumber = InvalidCardNumber,
				SellersCardNumber = InvalidCardNumber,
				BuyersFullName = InvalidFullName
			};

			_target.ConductPurchase(_payment);

			_mockOfPaymentRepository.Verify(m => m.Insert(_payment), Times.Once);
		}

		[TestMethod]
		public void ConductPurchase_CallsInsertOnce_WhenPaymentWithValidPropertiesIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			_target.ConductPurchase(_payment);

			_mockOfPaymentRepository.Verify(m => m.Insert(_payment), Times.Once);
		}

		[TestMethod]
		public void ConductPurchase_CallsLogPaymentOnce_WhenPaymentWithInvalidPropertiesIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				ExpirationMonth = InvalidInt,
				ExpirationYear = InvalidInt,
				BuyersCardNumber = InvalidCardNumber,
				SellersCardNumber = InvalidCardNumber,
				BuyersFullName = InvalidFullName
			};

			_target.ConductPurchase(_payment);

			_mockOfLogger.Verify(m => m.LogPayment(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void ConductPurchase_CallsLogPaymentOnce_WhenPaymentWithValidPropertiesIsPassed()
		{
			_account = new Account
			{
				ExpirationDate = DateTime.UtcNow,
				Owner = new User
				{
					FirstName = FirstName,
					LastName = LastName
				}
			};

			_mockOfAccountRepository.Setup(m => m.Contains(It.IsAny<Expression<Func<Account, bool>>>())).Returns(true);
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>())).Returns(_account);
			_payment = new Payment
			{
				PaymentAmount = ValidInt,
				ExpirationMonth = DateTime.UtcNow.Month,
				ExpirationYear = DateTime.UtcNow.Year,
				BuyersCardNumber = ValidCardNumber,
				SellersCardNumber = ValidCardNumber,
				BuyersFullName = ValidFullName
			};

			_target.ConductPurchase(_payment);

			_mockOfLogger.Verify(m => m.LogPayment(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void ConfirmPayment_ReturnsPaymentStatusTransactionIdIsNotCorrect_WhenInvalidPaymentIdIsPassed()
		{
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(t => t.Id == InvalidInt)).Returns<Payment>(null);

			var result = _target.ConfirmPayment(InvalidInt, ValidString);

			Assert.AreEqual(PaymentStatus.TransactionIdIsNotCorrect, result.PaymentStatus);
		}

		[TestMethod]
		public void ConfirmPayment_ReturnsPaymentStatusPending_WhenValidPaymentIdWithInvalidConfirmationCodeArePassed()
		{
			_payment = new Payment { ConfirmationCode = ValidString };
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(It.IsAny<Expression<Func<Payment, bool>>>())).Returns(_payment);

			var result = _target.ConfirmPayment(ValidInt, InvalidString);

			Assert.AreEqual(PaymentStatus.Pending, result.PaymentStatus);
		}

		[TestMethod]
		public void ConfirmPayment_ReturnsPaymentStatusSuccessful_WhenValidPaymentIdWithValidConfirmationCodeArePassed()
		{
			_payment = new Payment { ConfirmationCode = ValidString };
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(It.IsAny<Expression<Func<Payment, bool>>>())).Returns(_payment);

			var result = _target.ConfirmPayment(ValidInt, ValidString);

			Assert.AreEqual(PaymentStatus.Successful, result.PaymentStatus);
		}

		[TestMethod]
		public void ConfirmPayment_CallsUpdateOnce_WhenValidPaymentIdWithInValidConfirmationCodeArePassed()
		{
			_payment = new Payment { ConfirmationCode = ValidString };
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(It.IsAny<Expression<Func<Payment, bool>>>())).Returns(_payment);

			_target.ConfirmPayment(ValidInt, InvalidString);

			_mockOfPaymentRepository.Verify(m => m.Update(_payment), Times.Once);
		}

		[TestMethod]
		public void ConfirmPayment_CallsUpdateOnce_WhenValidPaymentIdWithValidConfirmationCodeArePassed()
		{
			_payment = new Payment { ConfirmationCode = ValidString };
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(It.IsAny<Expression<Func<Payment, bool>>>())).Returns(_payment);

			_target.ConfirmPayment(ValidInt, ValidString);

			_mockOfPaymentRepository.Verify(m => m.Update(_payment), Times.Once);
		}

		[TestMethod]
		public void ConfirmPayment_CallsLogPaymentOnce_WhenValidPaymentIdWithInValidConfirmationCodeArePassed()
		{
			_payment = new Payment { ConfirmationCode = ValidString };
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(It.IsAny<Expression<Func<Payment, bool>>>())).Returns(_payment);

			_target.ConfirmPayment(ValidInt, InvalidString);

			_mockOfLogger.Verify(m => m.LogPayment(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void ConfirmPayment_CallsLogPaymentOnce_WhenValidPaymentIdWithValidConfirmationCodeArePassed()
		{
			_payment = new Payment { ConfirmationCode = ValidString };
			_mockOfPaymentRepository.Setup(m => m.GetSingleOrDefault(It.IsAny<Expression<Func<Payment, bool>>>())).Returns(_payment);

			_target.ConfirmPayment(ValidInt, ValidString);

			_mockOfLogger.Verify(m => m.LogPayment(It.IsAny<string>()), Times.Once);
		}
	}
}