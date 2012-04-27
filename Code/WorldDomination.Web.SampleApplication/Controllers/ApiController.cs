using System;
using System.Collections.Generic;
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
            var pewPews = GetData(id) ?? new List<PewPew>();

            return new ApiJsonResult(new ApiViewModel
                                     {
                                         Items = new List<object>(pewPews),
                                         Page = pewPews.Count > 0 ? 1 : 0,
                                         PageSize = pewPews.Count > 0 ? 10 : 0,
                                         TotalItemsCount = pewPews.Count
                                     });
        }

        public JsonResult Error()
        {
            throw new InvalidOperationException("RuRoh - something unexpected happened.");
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