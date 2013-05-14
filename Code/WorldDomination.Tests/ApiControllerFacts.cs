using System.Collections.Generic;
using System.Linq;
using System.Net;
using WorldDomination.Web.Mvc.Results;
using WorldDomination.Web.SampleApplication.Controllers;
using WorldDomination.Web.SampleApplication.Models;
using Xunit;

namespace WorldDomination.Tests
{
    // ReSharper disable InconsistentNaming

    public class ApiControllerFacts
    {
        public class ErrorFacts
        {
            [Fact]
            public void GivenSomeBadParameters_Error2_RetunsSomeJsonWithA400Status()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                var errorJsonResult = apiController.Error2() as ErrorJsonResult;

                // Assert.
                Assert.NotNull(errorJsonResult);
                Assert.Equal(HttpStatusCode.BadRequest, errorJsonResult.ErrorStatus);

                // Retrieve the api data from 'items'.
                Assert.NotNull(errorJsonResult.ErrorMessages);
                Assert.NotEmpty(errorJsonResult.ErrorMessages);
                Assert.Equal("Error message #1.", errorJsonResult.ErrorMessage);
                Assert.Equal("Error message #1.", errorJsonResult.ErrorMessages.First().Value);
            }
        }

        public class IndexFacts
        {
            [Fact]
            public void GivenASingleUserDataObject_Index_ReturnsSomeJson()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                var apiJsonResult = apiController.Test(1) as ApiJsonResult;

                // Assert.
                Assert.NotNull(apiJsonResult);

                // Retrieve the api data from 'items'.
                var collection = apiJsonResult.Item as IList<object>;
                Assert.NotNull(collection);
                Assert.NotEmpty(collection);
                Assert.Equal(1, collection.Count);

                // Assert the data in the 'item's property.
                var pewPew = collection[0] as PewPew;
                Assert.NotNull(pewPew);
                Assert.Equal("Pure Krome", pewPew.Name);
                Assert.Equal(999, pewPew.Age);
                Assert.NotNull(pewPew.DanceMoves);
                Assert.Equal(2, pewPew.DanceMoves.Count);
                Assert.Equal("Melbourne Shuffle", pewPew.DanceMoves[0]);
            }

            [Fact]
            public void GivenACollectionOfUserData_Index_ReturnsSomeJson()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                var apiJsonResult = apiController.Test(2) as ApiJsonResult;

                // Assert.
                Assert.NotNull(apiJsonResult);

                // Retrieve the api data from 'items'.
                Assert.NotNull(apiJsonResult.Item);
                var collection = apiJsonResult.Item as IList<object>;
                Assert.NotNull(collection);
                Assert.NotEmpty(collection);
                Assert.Equal(2, collection.Count);

                // Assert the data in the 'item's property.
                var pewPew = collection[0] as PewPew;
                Assert.NotNull(pewPew);
                Assert.Equal("Pure Krome", pewPew.Name);
                Assert.Equal(999, pewPew.Age);
                Assert.NotNull(pewPew.DanceMoves);
                Assert.Equal(2, pewPew.DanceMoves.Count);
                Assert.Equal("Melbourne Shuffle", pewPew.DanceMoves[0]);

                // Paging Asserts.
                Assert.Equal(1, apiJsonResult.Page);
                Assert.Equal(10, apiJsonResult.PageSize);
                Assert.Equal(1, apiJsonResult.TotalPages);
                Assert.Equal(2, apiJsonResult.TotalItemsCount);
            }

            [Fact]
            public void GivenNoUserData_Index_ReturnsSomeJsonWithAnEmptyDataItems()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                var baseApiJsonResult = apiController.Test(0) as ApiJsonResult;

                // Assert.
                Assert.NotNull(baseApiJsonResult);
                var collection = baseApiJsonResult.Item as IList<object>;
                Assert.NotNull(collection);
                Assert.Empty(collection);
            }
        }
    }

    // ReSharper restore InconsistentNaming
}