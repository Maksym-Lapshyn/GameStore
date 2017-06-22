using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GameStore.Web.Infrastructure.Util
{
    public class LogUtility
    {
        public static string BuildException(Exception ex)
        {
            Exception logException = ex;
            if (ex.InnerException != null)
            {
                logException = ex.InnerException;
            }

            StringBuilder errorMessage = new StringBuilder(Environment.NewLine + "Error in Path :" + System.Web.HttpContext.Current.Request.Path);

            // Get the QueryString along with the Virtual Path
            errorMessage.Append(Environment.NewLine + "Raw Url :" + System.Web.HttpContext.Current.Request.RawUrl);

            // Get the error message
            errorMessage.Append(Environment.NewLine + "Message :" + logException.Message);

            // Source of the message
            errorMessage.Append(Environment.NewLine + "Source :" + logException.Source);

            // Stack Trace of the error
            errorMessage.Append(Environment.NewLine + "Stack Trace :" + logException.StackTrace);

            // Method where the error occurred
            errorMessage.Append(Environment.NewLine + "TargetSite :" + logException.TargetSite);
            return errorMessage.ToString();
        }
    }
}