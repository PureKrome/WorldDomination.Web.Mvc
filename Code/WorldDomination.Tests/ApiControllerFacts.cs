using System.Net;
using System.Web.Mvc;
using WorldDomination.Web.Mvc.Results;
using WorldDomination.Web.SampleApplication.Controllers;
using WorldDomination.Web.SampleApplication.Models;
using Xunit;

namespace WorldDomination.Tests
{
    // ReSharper disable InconsistentNaming

    public class ApiControllerFacts
    {
        public class IndexFacts
        {
            [Fact]
            public void GivenASingleUserDataObject_Index_ReturnsSomeJson()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                ApiJsonResult apiJsonResult = apiController.Test(1);

                // Assert.
                Assert.NotNull(apiJsonResult);
                Assert.NotNull(apiJsonResult.Data);
                Assert.Equal(HttpStatusCode.OK, apiJsonResult.HttpStatusCode);

                // Retrieve the api data from 'items'.
                dynamic data = apiJsonResult.Data;
                Assert.NotNull(data.items);
                Assert.Equal(1, data.items.Count);

                // Assert the data in the 'item's property.
                var pewPew = data.items[0] as PewPew;
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
                ApiJsonResult apiJsonResult = apiController.Test(2);

                // Assert.
                Assert.NotNull(apiJsonResult);
                Assert.NotNull(apiJsonResult.Data);
                Assert.Equal(HttpStatusCode.OK, apiJsonResult.HttpStatusCode);

                // Retrieve the api data from 'items'.
                dynamic data = apiJsonResult.Data;
                Assert.NotNull(data.items);
                Assert.Equal(2, data.items.Count);

                // Assert the data in the 'item's property.
                var pewPew = data.items[0] as PewPew;
                Assert.NotNull(pewPew);
                Assert.Equal("Pure Krome", pewPew.Name);
                Assert.Equal(999, pewPew.Age);
                Assert.NotNull(pewPew.DanceMoves);
                Assert.Equal(2, pewPew.DanceMoves.Count);
                Assert.Equal("Melbourne Shuffle", pewPew.DanceMoves[0]);

                // Paging Asserts.
                Assert.Equal(1, data.page);
                Assert.Equal(10, data.page_size);
                Assert.Equal(1, data.total_pages);
                Assert.Equal(2, data.total_items_count);
            }

            [Fact]
            public void GivenNoUserData_Index_ReturnsSomeJsonWithAnEmptyDataItemsAndStatusIs204()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                ApiJsonResult apiJsonResult = apiController.Test(0);

                // Assert.
                Assert.NotNull(apiJsonResult);
                Assert.NotNull(apiJsonResult.Data);

                // Retrieve the api data from 'items'.
                dynamic data = apiJsonResult.Data;
                Assert.Empty(data.items);
                Assert.Equal(HttpStatusCode.NoContent, apiJsonResult.HttpStatusCode);
            }
        }

        public class ErrorFacts
        {
            [Fact]
            public void GivenSomeBadParameters_Error2_RetunsSomeJsonWithA400Status()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                ApiJsonResult apiJsonResult = apiController.Error2();

                // Assert.
                Assert.NotNull(apiJsonResult);
                Assert.NotNull(apiJsonResult.Data);
                Assert.Equal(HttpStatusCode.BadRequest, apiJsonResult.HttpStatusCode);

                // Retrieve the api data from 'items'.
                dynamic data = apiJsonResult.Data;
                Assert.NotNull(data);
                Assert.Equal("Error message #1.\r\nError message #2.\r\nError message #3.\r\n", data.error_message);
            }
        }
    }

    // ReSharper restore InconsistentNaming
}