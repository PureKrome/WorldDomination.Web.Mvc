using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WorldDomination.Web.Mvc.Results
{
    public abstract class BaseApiJsonResult : JsonResult
    {
        protected BaseApiJsonResult()
        {
            MaximumQuota = -1;
            RemainingQuota = -1;
        }

        private JsonSerializerSettings _serializerSettings;
        public HttpStatusCode HttpStatusCode { get; protected set; }
        public Formatting Formatting { get; set; }
        public int MaximumQuota { get; set; }
        public int RemainingQuota { get; set; }

        public JsonSerializerSettings SerializerSettings
        {
            get
            {
                if (_serializerSettings != null)
                {
                    return _serializerSettings;
                }

                // New settings with all dates set to be ISO 8601.
                _serializerSettings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
                            {
                                new IsoDateTimeConverter()
                            }
                    };

                return _serializerSettings;
            }
            set { _serializerSettings = value; }
        }

        protected abstract dynamic SetData();

        public override void ExecuteResult(ControllerContext context)
        {
            dynamic data = SetData();
            if (MaximumQuota >= 0)
            {
                data.quota = MaximumQuota;
            }

            if (RemainingQuota >= 0)
            {
                data.quota_remaining = RemainingQuota;
            }

            Data = data;

            // This code is, more or less, a copy-paste job from JsonResult.ExecuteResult(..) method
            // except I've wired up my own JsonExpandoConverter.

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            // It's possible we might want to send back a 500 error with an error message, as json.
            // IIS express works ok, but IIS keeps trapping non 200's. So lets tell IIS to not handle non 200's.
            response.StatusCode = (int) HttpStatusCode;
            response.TrySkipIisCustomErrors = true;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data == null)
            {
                return;
            }

            var writer = new JsonTextWriter(response.Output) {Formatting = Formatting};

            var serializer = JsonSerializer.Create(SerializerSettings ?? new JsonSerializerSettings());
            serializer.Serialize(writer, Data);

            writer.Flush();
        }
    }
}