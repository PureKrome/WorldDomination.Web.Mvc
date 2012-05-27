using System;
using System.Collections.Generic;

namespace WorldDomination.Web.Mvc.Models
{
    public class ApiViewModel : BaseApiViewModel
    {
        public IList<object> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPages
        {
            get { return TotalItemsCount <= 0 ? 0 : (int) Math.Ceiling(TotalItemsCount/(double) (PageSize)); }
        }

        public int TotalItemsCount { get; set; }
    }
}