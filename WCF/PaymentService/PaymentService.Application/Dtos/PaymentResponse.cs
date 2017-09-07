using Common.Entities;
using System.Runtime.Serialization;

namespace PaymentService.Application.Dtos
{
	[DataContract]
	public class PaymentResponse
	{
		[DataMember]
		public int PaymentId { get; set; }

		[DataMember]
		public PaymentStatus PaymentStatus { get; set; }
	}
}