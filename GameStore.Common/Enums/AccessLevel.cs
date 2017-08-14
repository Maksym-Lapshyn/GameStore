using System;

namespace GameStore.Common.Enums
{
	[Flags]
	public enum AccessLevel
	{
		Administrator,
		Manager,
		Moderator,
		User
	}
}