using Common.Abstract;
using Common.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
		private const string InvalidBuyersName = "test";
		private const string ValidBuyersName = "John Doe";

		private Mock<I>
		private Mock<IAccountRepository> _mockOfAccountRepository;
		private Mock<IPaymentRepository> _mockOfPaymentRepository;
		private Mock<ILogger> _mockOfLogger;
		private PaymentService _target;
		private Payment _payment;

		[TestInitialize]
		public void Initialize()
		{
			_mockOfAccountRepository = new Mock<IAccountRepository>();
			_mockOfPaymentRepository = new Mock<IPaymentRepository>();
			_mockOfLogger = new Mock<ILogger>();
			_target = new PaymentService(_mockOfPaymentRepository.Object, _mockOfAccountRepository.Object, _mockOfLogger.Object);
		}

		[TestMethod]
		public void ConductPurchase_ReturnsPaymentStatusNotSupportedCard_WhenInvalidCardNumbersArePassed()
		{
			_mockOfAccountRepository.Setup(m => m.GetSingle(It.IsAny<Expression<Func<Account, bool>>>()))
				.Returns(new Account {Balance = 0});
			_payment = new Payment
			{
				BuyersCardNumber = InvalidCardNumber,
				BuyersFullName = ValidC
			};
		}
	}
}