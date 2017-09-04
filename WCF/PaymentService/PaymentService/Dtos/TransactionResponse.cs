using System;
using System.Runtime.Serialization;

namespace PaymentService.Dtos
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