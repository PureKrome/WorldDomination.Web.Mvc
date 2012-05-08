using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace WorldDomination.Web.Mvc.Models
{
    // ReSharper disable InconsistentNaming

    public class ErrorViewModel : BaseApiViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(IDictionary<string, ModelState> modelStateDictionary)
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
        }

        public HttpStatusCode ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
    }

    // ReSharper restore InconsistentNaming
}