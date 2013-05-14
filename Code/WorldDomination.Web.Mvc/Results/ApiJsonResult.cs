using System;
using System.Dynamic;
using System.Net;

namespace WorldDomination.Web.Mvc.Results
{
    public class ApiJsonResult : BaseApiJsonResult
    {
        public ApiJsonResult()
        {
            Page = 1;
            PageSize = 10;
        }

        public object Item { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int TotalPages
        {
            get { return TotalItemsCount <= 0 ? 0 : (int) Math.Ceiling(TotalItemsCount/(double) (PageSize)); }
        }

        public int TotalItemsCount { get; set; }

        protected override dynamic SetData()
        {
            dynamic data = new ExpandoObject();
            data.items = Item ?? new object(); // NOTE: can be empty.

            HttpStatusCode = HttpStatusCode.OK;

            if (Page > 0)
            {
                data.page = Page;
            }

            if (PageSize > 0)
            {
                data.page_size = PageSize;
            }

            if (TotalPages > 0)
            {
                data.total_pages = TotalPages;
            }

            if (TotalItemsCount > 0)
            {
                data.total_items_count = TotalItemsCount;
            }

            return data;
        }
    }
}