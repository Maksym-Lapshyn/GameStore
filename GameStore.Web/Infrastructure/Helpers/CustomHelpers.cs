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
				div.InnerHtml += $"<p><label for={name}><input type='checkbox' id={name} name={propertyName} value={id} checked /> " + name + "</label></p>";
			}
			else
			{
				div.InnerHtml += $"<p><label for={name}><input type='checkbox' id={name} name={propertyName} value={id} /> " + name + "</label></p>";
			}

			return new MvcHtmlString(div.InnerHtml);
		}

		public static MvcHtmlString CreateRadioButtonGroup(this HtmlHelper helper, string propertyName, Enum dateOptions)
		{
			var div = new TagBuilder("div");

			foreach (var option in Enum.GetNames(dateOptions.GetType()))
			{
				if (option == dateOptions.ToString())
				{
					div.InnerHtml += $"<p><label for={option}><input type='radio' id={option} name={propertyName} value={option} checked /> " + option + "</label></p>";
				}
				else
				{
					div.InnerHtml += $"<p><label for={option}><input type='radio' id={option} name={propertyName} value={option} /> " + option + "</label></p>";
				}
			}

			return new MvcHtmlString(div.ToString());
		}
	}
}