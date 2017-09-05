using System;
using System.Runtime.Serialization;

namespace PaymentService.Application.Dtos
{
	[DataContract]
	public class TransactionResponse
	{
		[DataMember]
		public Guid TransactionId { get; set; }

		[DataMember]
		public PaymentStatus PaymentStatus { get; set; }
	}
}