using System.Web.Mvc;
using WorldDomination.Web.SampleApplication.Controllers;
using WorldDomination.Web.SampleApplication.Models;
using Xunit;

namespace WorldDomination.Tests
{
    // ReSharper disable InconsistentNaming

    public class ApiControllerFacts
    {
        #region Nested type: IndexFacts

        public class IndexFacts
        {
            [Fact]
            public void GivenSomeUserData_Index_ReturnsSomeJson()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                JsonResult jsonResult = apiController.Index();

                // Assert.
                Assert.NotNull(jsonResult);
                Assert.NotNull(jsonResult.Data);

                // Retrieve the api data from 'items'.
                dynamic data = jsonResult.Data;
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
            public void GivenSomeUserData_Index2_ReturnsSomeJson()
            {
                // Arrange.
                var apiController = new ApiController();

                // Act.
                JsonResult jsonResult = apiController.Index2();

                // Assert.
                Assert.NotNull(jsonResult);
                Assert.NotNull(jsonResult.Data);

                // Retrieve the api data from 'items'.
                dynamic data = jsonResult.Data;
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
                Assert.Equal(10, data.total_pages);
                Assert.Equal(100, data.total_items_count);
            }
        }

        #endregion
    }

    // ReSharper restore InconsistentNaming
}