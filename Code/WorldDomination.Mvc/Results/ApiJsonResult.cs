using System;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WorldDomination.Mvc.Models;

namespace WorldDomination.Mvc.Results
{
    public class ApiJsonResult : JsonResult
    {
        public ApiJsonResult(ApiViewModel responseWrapper)
        {
            if (responseWrapper == null)
            {
                throw new ArgumentNullException("responseWrapper");
            }

            dynamic data = new ExpandoObject();

            // Required Items.
            data.items = responseWrapper.Items;
            data.quota = responseWrapper.MaximumQuota;
            data.quota_remaining = responseWrapper.RemainingQuota;

            // Optional stuff.
            if (responseWrapper.Page > 0)
            {
                data.page = responseWrapper.Page;
            }

            if (responseWrapper.PageSize > 0)
            {
                data.page_size = responseWrapper.PageSize;
            }

            if (responseWrapper.TotalPages > 0)
            {
                data.total_pages = responseWrapper.TotalPages;
            }

            if (responseWrapper.TotalItemsCount > 0)
            {
                data.total_items_count = responseWrapper.TotalItemsCount;
            }

            Data = data;
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            // This code is, more or less, a copy-paste job from JsonResult.ExecuteResult(..) method
            // except I've wired up my own JsonExpandoConverter.

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.RegisterConverters(new JavaScriptConverter[] {new JsonExpandoConverter()});
            response.Write(javaScriptSerializer.Serialize(Data));
        }
    }
}