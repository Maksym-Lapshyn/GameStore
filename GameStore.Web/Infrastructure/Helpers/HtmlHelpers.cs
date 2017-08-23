using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GameStore.Web.Infrastructure.Helpers
{
	public static class HtmlHelpers
	{
		public static MvcHtmlString CreateCheckbox(this HtmlHelper helper, string propertyName, string name, List<string> selectedItems)
		{
			var divTagBuilder = new TagBuilder("div");

			if (selectedItems.Contains(name))
			{
				divTagBuilder.InnerHtml += $"<p><label for=\"{name}\"><input type='checkbox' id=\"{name}\" name=\"{propertyName}\" value=\"{name}\" checked /> " + name + "</label></p>";
			}
			else
			{
				divTagBuilder.InnerHtml += $"<p><label for=\"{name}\"><input type='checkbox' id=\"{name}\" name=\"{propertyName}\" value=\"{name}\" /> " + name + "</label></p>";
			}

			return new MvcHtmlString(divTagBuilder.InnerHtml);
		}

		public static MvcHtmlString CreateRadioButton(this HtmlHelper helper, string propertyName, string name, string selectedItem)
		{
			var divTagBuilder = new TagBuilder("div");

			if (name == selectedItem)
			{
				divTagBuilder.InnerHtml += $"<p><label for=\"{name}\"><input type='radio' id=\"{name}\" name=\"{propertyName}\" value=\"{name}\" checked /> " + name + "</label></p>";
			}
			else
			{
				divTagBuilder.InnerHtml += $"<p><label for=\"{name}\"><input type='radio' id=\"{name}\" name=\"{propertyName}\" value=\"{name}\" /> " + name + "</label></p>";
			}

			return new MvcHtmlString(divTagBuilder.InnerHtml);
		}

		public static MvcHtmlString CreateRadioButtonGroup(this HtmlHelper helper, string propertyName, Enum value)
		{
			var divTagBuilder = new TagBuilder("div");

			foreach (var option in Enum.GetNames(value.GetType()))
			{
				if (option == value.ToString())
				{
					divTagBuilder.InnerHtml += $"<p><label for={option}><input type='radio' id={option} name={propertyName} value={option} checked /> " + option + "</label></p>";
				}
				else
				{
					divTagBuilder.InnerHtml += $"<p><label for={option}><input type='radio' id={option} name={propertyName} value={option} /> " + option + "</label></p>";
				}
			}

			return new MvcHtmlString(divTagBuilder.ToString());
		}
	}
}