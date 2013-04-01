using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace WorldDomination.Web.Mvc.Results
{
    public class ErrorJsonResult : BaseApiJsonResult
    {
        public ErrorJsonResult()
        {
            ErrorStatus = HttpStatusCode.InternalServerError;
        }

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

            var errorMessage = new StringBuilder();
            foreach (ModelError error in modelStateDictionary.Values.SelectMany(x => x.Errors))
            {
                if (errorMessage.Length > 0)
                {
                    errorMessage.Append(" ");
                }

                errorMessage.Append(error.ErrorMessage);
            }

            ErrorMessage = errorMessage.ToString();
            ErrorStatus = errorStatus;
        }

        public ErrorJsonResult(HttpStatusCode errorStatus, string errorMessage)
        {
            ErrorStatus = errorStatus;
            ErrorMessage = errorMessage;
        }

        public HttpStatusCode ErrorStatus { get; private set; }
        public string ErrorMessage { get; private set; }

        protected override dynamic SetData()
        {
            dynamic data = new ExpandoObject();
            data.error_message = ErrorMessage;

            return data;
        }
    }
}