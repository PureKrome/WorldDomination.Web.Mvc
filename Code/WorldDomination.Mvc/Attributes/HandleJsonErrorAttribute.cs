using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WorldDomination.Web.Mvc.Models;
using WorldDomination.Web.Mvc.Results;

namespace WorldDomination.Web.Mvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class HandleJsonErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public object Data { get; set; }

        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            if (filterContext.IsChildAction)
            {
                return;
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information. 
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (filterContext.Exception == null)
            {
                throw new ArgumentException("filterContext.Exception is null or missing.");
            }

            Exception exception = filterContext.Exception;

            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            if (exception is HttpException)
            {
                httpStatusCode = (HttpStatusCode) ((HttpException) exception).GetHttpCode();
            }

            // Grab all the error messages.
            var errorMessages = new StringBuilder();
            while (exception != null)
            {
                errorMessages.AppendLine(exception.Message);
                exception = exception.InnerException;
            }

            filterContext.Result = new ApiJsonResult(new ErrorViewModel
                                                     {
                                                         ErrorMessage = errorMessages.ToString(),
                                                         ErrorStatus = httpStatusCode
                                                     });

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int) httpStatusCode;

            // Certain versions of IIS will sometimes use their own error page when 
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        #endregion
    }
}