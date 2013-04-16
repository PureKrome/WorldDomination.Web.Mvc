using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WorldDomination.Web.Mvc.Results
{
    public class ErrorJsonResult : BaseApiJsonResult
    {
        public ErrorJsonResult(IDictionary<string, ModelState> modelStateDictionary)
            : this(HttpStatusCode.InternalServerError, modelStateDictionary)
        {
        }

        public ErrorJsonResult(HttpStatusCode errorStatus, IDictionary<string, ModelState> modelStateDictionary)
        {
            if (modelStateDictionary == null)
            {
                throw new ArgumentNullException("modelStateDictionary");
            }

            if (modelStateDictionary.Count <= 0)
            {
                return;
            }

            HttpStatusCode = errorStatus;
            ErrorMessages = new Dictionary<string, string>();
            var i = 0;
            foreach (var errorMessage in modelStateDictionary.Values.SelectMany(x => x.Errors))
            {
                ErrorMessages.Add(string.Format("Error-{0}", ++i), errorMessage.ErrorMessage);
            }
        }

        public ErrorJsonResult(HttpStatusCode errorStatus, string errorMessage)
            : this(errorStatus, new Dictionary<string, string> {{"Error", errorMessage}})
        {
        }

        public ErrorJsonResult(HttpStatusCode errorStatus, ICollection<string> errorMessages)
        {
            if (errorMessages == null ||
                !errorMessages.Any())
            {
                throw new ArgumentNullException("errorMessages", "Must provide at least one error message key/value.");
            }

            HttpStatusCode = errorStatus;
            ErrorMessages = new Dictionary<string, string>();
            var i = 0;
            foreach (var errorMessage in errorMessages)
            {
                ErrorMessages.Add(string.Format("Error-{0}", ++i), errorMessage);
            }
        }

        public ErrorJsonResult(HttpStatusCode errorStatus, IDictionary<string, string> errorMessages)
        {
            if (errorMessages == null ||
                errorMessages.Count <= 0)
            {
                throw new ArgumentNullException("errorMessages", "Must provide at least one error message key/value.");
            }

            HttpStatusCode = errorStatus;
            ErrorMessages = errorMessages;
        }

        public HttpStatusCode ErrorStatus
        {
            get { return HttpStatusCode; }
        }

        public string ErrorMessage
        {
            get { return ErrorMessages.FirstOrDefault().Value; }
        }

        public IDictionary<string, string> ErrorMessages { get; private set; }

        protected override dynamic SetData()
        {
            dynamic data = new ExpandoObject();
            data.error_messages = (from error in ErrorMessages
                                   select new
                                              {
                                                  key = error.Key,
                                                  error_message = error.Value
                                              }).ToList();

            return data;
        }
    }
}