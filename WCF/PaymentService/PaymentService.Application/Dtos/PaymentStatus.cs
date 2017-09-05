using System.Runtime.Serialization;

namespace PaymentService.Application.Dtos
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
		Pending,

		[EnumMember]
		ConfirmationCodeIsNotCorrect
	}
}