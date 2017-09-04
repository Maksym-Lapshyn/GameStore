using System.Runtime.Serialization;

namespace PaymentService.Dtos
{
	[DataContract]
	public enum PaymentStatus
	{
		[EnumMember]
		Successful,

		[EnumMember]
		NotEnoughMoney,

		[EnumMember]
		CardDoesNotExist,

		[EnumMember]
		PaymentFailed,

		[EnumMember]
		Pending
	}
}