using PaymentService.Dtos;
using System.ServiceModel;

namespace PaymentService.Services.Abstract
{
	[ServiceContract]
	public interface IPayment
	{
		[OperationContract]
		TransactionResponse ConductPurchase(Transaction transaction);

		[OperationContract]
		TransactionResponse ConfirmTransaction(int transactionId, int confirmationCode);
	}
}