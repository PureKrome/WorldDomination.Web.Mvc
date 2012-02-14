using System.Collections.Generic;

namespace WorldDomination.Mvc.Models
{
    public class ResponseWrapper
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