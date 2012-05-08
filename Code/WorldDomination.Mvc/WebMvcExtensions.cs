using System;
using System.Web;

namespace WorldDomination.Web.Mvc
{
    public static class WebMvcExtensions
    {
        public static void Location(this HttpResponseBase value, HttpRequestBase requestBase, string path)
        {
            if (requestBase == null || requestBase.Url == null)
            {
                throw new ArgumentNullException("requestBase");
            }

            var urlBuilder = new UriBuilder(requestBase.Url.AbsoluteUri)
                             {
                                 Path = path
                             };

            value.AddHeader("Location", urlBuilder.Uri.AbsoluteUri);
        }
    }
}