using Common.Entities;
using PaymentService.Application.Dtos;
using System.ServiceModel;

namespace PaymentService.Application
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract]
	public interface IPaymentService
	{
		[OperationContract]
		PaymentResponse ConductPurchase(Payment payment);

		[OperationContract]
		PaymentResponse ConfirmPayment(int paymentId, string confirmationCode);
	}
}