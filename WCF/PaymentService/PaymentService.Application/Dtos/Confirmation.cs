using System.Runtime.Serialization;

namespace PaymentService.Application.Dtos
{
	[DataContract]
	public class Confirmation
	{
		[DataMember]
		public int PaymentId { get; set; }

		[DataMember]
		public string ConfirmationCode { get; set; }
	}
}