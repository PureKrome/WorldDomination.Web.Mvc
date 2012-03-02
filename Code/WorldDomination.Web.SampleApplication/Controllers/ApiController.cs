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
        public JsonResult Index()
        {
            var pewPew = new PewPew
                             {
                                 Name = "Pure Krome",
                                 Age = 999,
                                 DanceMoves = new List<string>
                                                  {
                                                      "Melbourne Shuffle",
                                                      "Moonwalk"
                                                  }
                             };
            return new ApiJsonResult(new ApiViewModel
                                         {
                                             Items = new List<object> {pewPew}
                                         });
        }

        // Example of something returning a collection of results.
        // Eg. a list of users, a list of products, a list of <insert what eva, here>.
        // We assume that you're paging your results, of course....
        public JsonResult Index2()
        {
            var dancingPeople = new List<PewPew>
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

            return new ApiJsonResult(new ApiViewModel
                                         {
                                             Items = new List<object> (dancingPeople),
                                             Page = 1,
                                             PageSize = 10,
                                             TotalItemsCount = 55
                                         });
        }

        public JsonResult Error1()
        {
            throw new InvalidOperationException("RuRoh - something unexpected happened.");
        }
    }
}