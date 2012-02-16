using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace WorldDomination.Mvc.Results
{
    public class ErrorJsonResult : JsonResult
    {
        private HttpStatusCode _httpStatusCode;

        public ErrorJsonResult(HttpStatusCode httpStatusCode, ICollection<string> errorMessages)
        {
            if (errorMessages == null)
            {
                throw new ArgumentNullException("errorMessages");
            }

            // Go directly to the member variable so we avoid calling SetDataObject too many times.
            _httpStatusCode = httpStatusCode;
            ErrorMessages = errorMessages;

            JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            // Ok, now we set the data object.
            SetDataObject();
        }

        public ErrorJsonResult(HttpStatusCode httpStatusCode, string errorMessage)
            : this(httpStatusCode, new List<string> {errorMessage})
        {
        }

        public HttpStatusCode HttpStatusCode
        {
            get { return _httpStatusCode; }
            set
            {
                _httpStatusCode = value;
                SetDataObject();
            }
        }

        public ICollection<string> ErrorMessages { get; private set; }

        public void AddErrorMessage(string errorMessage)
        {
            if (ErrorMessages == null)
            {
                ErrorMessages = new List<string>();
            }

            ErrorMessages.Add(errorMessage);

            SetDataObject();
        }

        public new object Data { get; private set; }

        private void SetDataObject()
        {
            var errorMessages = new StringBuilder();
            if (ErrorMessages != null)
            {
                foreach (string errorMessage in ErrorMessages)
                {
                    errorMessages.Append(errorMessage);
                }
            }

            dynamic data = new ExpandoObject();
            data.error_status = (int) HttpStatusCode;
            data.error_name = GetErrorName(HttpStatusCode);
            data.error_message = errorMessages.ToString();

            Data = data;
        }

        private static string GetErrorName(HttpStatusCode httpStatusCode)
        {
            string description;
            switch ((int) httpStatusCode)
            {
                case 400:
                    description = "bad_parameter";
                    break;
                case 401:
                    description = "access_token_required";
                    break;
                case 402:
                    description = "invalid_access_token";
                    break;
                case 403:
                    description = "access_denied";
                    break;
                case 404:
                    description = "no_method";
                    break;
                case 405:
                    description = "key_required";
                    break;
                case 406:
                    description = "access_token_compromised";
                    break;
                case 500:
                    description = "internal_error";
                    break;
                case 502:
                    description = "throttle_violation";
                    break;
                default:
                    description = "unknown_error";
                    break;
            }

            return description;
        }
    }
}