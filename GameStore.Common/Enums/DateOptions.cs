using System.ComponentModel;

namespace GameStore.Common.Enums
{
	public enum DateOptions
	{
		None,
		[Description("Last week")]
		LastWeek,
		[Description("Last month")]
		LastMonth,
		[Description("Last year")]
		LastYear,
		[Description("Two years ago")]
		TwoYears,
		[Description("Three years ago")]
		ThreeYears
	}
}