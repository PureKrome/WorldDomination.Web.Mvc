using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WorldDomination.Web.Mvc.Models;

namespace WorldDomination.Web.Mvc.Results
{
    public class ApiJsonResult : JsonResult
    {
        public ApiJsonResult(BaseApiViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            dynamic data = new ExpandoObject();

            // Optional stuff => lets convert this viewModel.
            var apiViewModel = viewModel as ApiViewModel;
            if (apiViewModel != null)
            {
                data.items = apiViewModel.Items ?? new List<object>(); // NOTE: can be empty.

                //HttpStatusCode = apiViewModel.Items == null || apiViewModel.Items.Count <= 0
                //                     ? HttpStatusCode.NoContent
                //                     : HttpStatusCode.OK;
                HttpStatusCode = HttpStatusCode.OK;

                if (apiViewModel.Page > 0)
                {
                    data.page = apiViewModel.Page;
                }

                if (apiViewModel.PageSize > 0)
                {
                    data.page_size = apiViewModel.PageSize;
                }

                if (apiViewModel.TotalPages > 0)
                {
                    data.total_pages = apiViewModel.TotalPages;
                }

                if (apiViewModel.TotalItemsCount > 0)
                {
                    data.total_items_count = apiViewModel.TotalItemsCount;
                }
            }

            var errorViewModel = viewModel as ErrorViewModel;
            if (errorViewModel != null)
            {
                data.error_message = errorViewModel.ErrorMessage;
                HttpStatusCode = errorViewModel.ErrorStatus;
            }

            // Required Items.
            data.quota = viewModel.MaximumQuota;
            data.quota_remaining = viewModel.RemainingQuota;

            Data = data;

            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public HttpStatusCode HttpStatusCode { get; private set; }

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
            response.StatusCode = (int) HttpStatusCode;

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