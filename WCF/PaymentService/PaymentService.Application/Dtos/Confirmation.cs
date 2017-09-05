using System;
using System.Runtime.Serialization;

namespace PaymentService.Application.Dtos
{
	[DataContract]
	public class Confirmation
	{
		[DataMember]
		public Guid TransactionId { get; set; }

		[DataMember]
		public int ConfirmationCode { get; set; }
	}
}