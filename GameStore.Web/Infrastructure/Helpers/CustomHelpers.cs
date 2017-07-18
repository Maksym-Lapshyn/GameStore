using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Helpers
{
	public static class CustomHelpers
	{
		public static MvcHtmlString CreateCheckbox(this HtmlHelper helper, string propertyName, string name, int id, List<int> selectedItems)
		{
			var div = new TagBuilder("div");

			if (selectedItems.Contains(id))
			{
				div.InnerHtml += $"<input type='checkbox' id={id} name={propertyName} value={id} checked />";
			}
			else
			{
				div.InnerHtml += $"<input type='checkbox' id={id} name={propertyName} value={id} />";
			}

			div.InnerHtml += $"<label for={id}>" + name + "</label>";

			return new MvcHtmlString(div.InnerHtml);
		}

		public static MvcHtmlString CreateRadioButtonGroup(this HtmlHelper helper, string propertyName, Enum dateOptions)
		{
			var div = new TagBuilder("div");

			foreach (var option in Enum.GetNames(dateOptions.GetType()))
			{
				if (option == dateOptions.ToString())
				{
					div.InnerHtml += $"<input type='radio' name={propertyName} value={option} checked />";
				}
				else
				{
					div.InnerHtml += $"<input type='radio' name={propertyName} value={option} />";
				}

				div.InnerHtml += $"<label for={option}>" + option + "</label>";
			}

			return new MvcHtmlString(div.ToString());
		}
	}
}