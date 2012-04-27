using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;
using WorldDomination.Web.Mvc.Models;
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

        public ApiJsonResult Test(int id)
        {
            IList<PewPew> pewPews = GetData(id) ?? new List<PewPew>();

            return new ApiJsonResult(new ApiViewModel
                                         {
                                             Items = new List<object>(pewPews),
                                             Page = pewPews.Count > 0 ? 1 : 0,
                                             PageSize = pewPews.Count > 0 ? 10 : 0,
                                             TotalItemsCount = pewPews.Count
                                         });
        }

        public ApiJsonResult Error1()
        {
            throw new InvalidOperationException("RuRoh - something unexpected happened.");
        }

        public ApiJsonResult Error2()
        {
            var errors = new StringBuilder();
            errors.AppendLine("Error message #1.");
            errors.AppendLine("Error message #2.");
            errors.AppendLine("Error message #3.");
            var errorApi = new ErrorViewModel
                               {
                                   ErrorMessage = errors.ToString(),
                                   ErrorStatus = HttpStatusCode.BadRequest
                               };

            return new ApiJsonResult(errorApi);
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
                                                               }
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
                                                               }
                                          },
                                      new PewPew
                                          {
                                              Name = "AssHat",
                                              Age = 999,
                                              DanceMoves = new List<string>
                                                               {
                                                                   "Sprinkler",
                                                                   "Small Box, Middle Box, Large Box"
                                                               }
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