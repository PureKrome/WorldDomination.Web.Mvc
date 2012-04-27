using System.Net;

namespace WorldDomination.Web.Mvc.Models
{
    // ReSharper disable InconsistentNaming

    public class ErrorViewModel : BaseApiViewModel
    {
        public HttpStatusCode ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
    }

    // ReSharper restore InconsistentNaming
}