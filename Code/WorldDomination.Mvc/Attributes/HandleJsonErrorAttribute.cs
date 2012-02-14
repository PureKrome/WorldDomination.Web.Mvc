using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldDomination.Mvc.Results;

namespace WorldDomination.Mvc.Attributes
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
            var errorMessages = new List<string>();
            while (exception != null)
            {
                errorMessages.Add(exception.Message);
                exception = exception.InnerException;
            }

            filterContext.Result = new ErrorJsonResult(httpStatusCode, errorMessages);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = (int) httpStatusCode;

            // Certain versions of IIS will sometimes use their own error page when 
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        #endregion

        public static IEnumerable<HttpStatusCode> AcceptedHttpErrorCodes()
        {
            return new List<HttpStatusCode>
                       {
                           HttpStatusCode.BadRequest,
                           HttpStatusCode.Unauthorized,
                           HttpStatusCode.PaymentRequired,
                           HttpStatusCode.Forbidden,
                           HttpStatusCode.NotFound,
                           HttpStatusCode.MethodNotAllowed,
                           HttpStatusCode.NotAcceptable,
                           HttpStatusCode.InternalServerError,
                           HttpStatusCode.BadGateway
                       };
        }
    }
}