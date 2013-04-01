using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;
using WorldDomination.Web.Mvc.Attributes;
using WorldDomination.Web.Mvc.Results;
using WorldDomination.Web.SampleApplication.Models;

namespace WorldDomination.Web.SampleApplication.Controllers
{
    public class ApiController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public BaseApiJsonResult Test(int id)
        {
            IList<PewPew> pewPews = GetData(id) ?? new List<PewPew>();

            return new ApiJsonResult
                {
                    Items = new List<object>(pewPews),
                    Page = pewPews.Count > 0 ? 1 : 0,
                    PageSize = pewPews.Count > 0 ? 10 : 0,
                    TotalItemsCount = pewPews.Count
                };
        }

        [HandleJsonError]
        public BaseApiJsonResult Error1()
        {
            throw new InvalidOperationException("RuRoh - something unexpected happened.");
        }

        public BaseApiJsonResult Error2()
        {
            var errors = new StringBuilder();
            errors.AppendLine("Error message #1.");
            errors.AppendLine("Error message #2.");
            errors.AppendLine("Error message #3.");

            return new ErrorJsonResult(HttpStatusCode.BadRequest, errors.ToString());
        }

        private static IList<PewPew> GetData(int id)
        {
            List<PewPew> pewPews;

            switch (id)
            {
                case 1:
                    pewPews = new List<PewPew>
                        {
                            new PewPew
                                {
                                    Name = "Pure Krome",
                                    Age = 999,
                                    DanceMoves = new List<string>
                                        {
                                            "Melbourne Shuffle",
                                            "Moonwalk"
                                        },
                                    CreatedOn = DateTime.UtcNow
                                }
                        };
                    break;
                case 2:
                    pewPews = new List<PewPew>
                        {
                            new PewPew
                                {
                                    Name = "Pure Krome",
                                    Age = 999,
                                    DanceMoves = new List<string>
                                        {
                                            "Melbourne Shuffle",
                                            "Moonwalk"
                                        },
                                    CreatedOn = DateTime.UtcNow
                                },
                            new PewPew
                                {
                                    Name = "AssHat",
                                    Age = 999,
                                    DanceMoves = new List<string>
                                        {
                                            "Sprinkler",
                                            "Small Box, Middle Box, Large Box"
                                        },
                                    CreatedOn = DateTime.UtcNow
                                }
                        };
                    break;
                default:
                    pewPews = null;
                    break;
            }

            return pewPews;
        }
    }
}