using System.Runtime.Serialization;

namespace Common.Entities
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
		TransactionIdIsNotCorrect,

		[EnumMember]
		NotSupportedCard
	}
}