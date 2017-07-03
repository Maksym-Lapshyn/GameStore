using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.Models;
using System.Web.Mvc;
using Ninject;
using System.Linq;
using System.Web;

namespace GameStore.Web.Infrastructure.Attributes
{
    public class CannotBeEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as List<int>;

            return list != null && list.Count > 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"The {name} field is required";
        }
    }
}