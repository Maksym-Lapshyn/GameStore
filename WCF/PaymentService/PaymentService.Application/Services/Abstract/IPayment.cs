using PaymentService.Application.Dtos;
using System;
using System.ServiceModel;

namespace PaymentService.Application.Services.Abstract
{
	[ServiceContract]
	public interface IPayment
	{
		[OperationContract]
		TransactionResponse ConductPurchase(Transaction transaction);

		[OperationContract]
		TransactionResponse ConfirmTransaction(Guid transactionId, int confirmationCode);
	}
}