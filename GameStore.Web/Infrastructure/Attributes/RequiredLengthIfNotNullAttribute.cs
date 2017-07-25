using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Infrastructure.Attributes
{
	public class RequiredLengthIfNotNullAttribute : ValidationAttribute
	{
		private readonly int _requiredLength;

		public RequiredLengthIfNotNullAttribute(int requiredLength)
		{
			_requiredLength = requiredLength;
		}

		public override bool IsValid(object value)
		{
			if (value is string gameName)
			{
				return gameName.Length >= _requiredLength;
			}

			return true;
		}

		public override string FormatErrorMessage(string name)
		{
			return $"The {name} field should be longer than {_requiredLength} characters";
		}
	}
}