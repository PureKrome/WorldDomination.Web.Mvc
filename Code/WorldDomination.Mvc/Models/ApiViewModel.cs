using System.Collections.Generic;

namespace WorldDomination.Web.Mvc.Models
{
    public class ApiViewModel
    {
        public int ErrorStatus { get; set; }
        public string ErrorMessage { get; set; }
        public IList<object> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItemsCount { get; set; }
        public int MaximumQuota { get; set; }
        public int RemainingQuota { get; set; }
    }
}