using System.Runtime.Serialization;

namespace Common.Entities
{
	[DataContract]
	public class Payment
	{
		public int Id { get; set; }

		public PaymentStatus PaymentStatus { get; set; }

		public string ConfirmationCode { get; set; }

		[DataMember]
		public string SellersCardNumber { get; set; }

		[DataMember]
		public string BuyersCardNumber { get; set; }

		[DataMember]
		public string BuyersFullName { get; set; }

		[DataMember]
		public int CvvCode { get; set; }

		[DataMember]
		public int ExpirationMonth { get; set; }

		[DataMember]
		public int ExpirationYear { get; set; }

		[DataMember]
		public string PaymentPurpose { get; set; }

		[DataMember]
		public decimal PaymentAmount { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string Phone { get; set; }
	}
}