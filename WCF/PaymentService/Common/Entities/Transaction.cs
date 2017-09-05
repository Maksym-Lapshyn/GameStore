using System.Runtime.Serialization;

namespace Common.Entities
{
	[DataContract]
	public class Transaction
	{
		public int Id { get; set; }

		public PaymentStatus PaymentStatus { get; set; }

		public bool IsDeleted { get; set; }

		[DataMember]
		public long ConsumerCardNumber { get; set; }

		[DataMember]
		public long SupplierCardNumber { get; set; }

		[DataMember]
		public string SupplierFullName { get; set; }

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