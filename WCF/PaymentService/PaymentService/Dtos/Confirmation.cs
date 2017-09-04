using System;
using System.Runtime.Serialization;

namespace PaymentService.Dtos
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