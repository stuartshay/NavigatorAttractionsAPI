using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Filters;
using NavigatorAttractions.WebAPI.Test.Data;
using System.Threading.Tasks;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class AttractionControllerTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_ReturnsData()
        {
            var dataSet = AttractionDataSet.GetAttractionModel();

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .ReturnsAsync(dataSet);

            var controller = GetAttractionController(attractionService.Object);

            // Act
            var sut = await controller.Get(dataSet.Id);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<AttractionModel>(objectResult.Value);

            var result = objectResult.Value as AttractionModel;
            Assert.NotNull(result);
            Assert.Equal(dataSet.Id, result.Id);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_Returns_BadRequet()
        {
            var controller = GetAttractionController();

            // Act
            var sut = await controller.Get(string.Empty);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_Returns_NotFound()
        {
            var dataSet = AttractionDataSet.GetAttractionModel();

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .Returns(Task.FromResult((AttractionModel)null));

            var controller = GetAttractionController(attractionService.Object);

            // Act
            var sut = await controller.Get(dataSet.Id);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<NotFoundResult>(sut);

            var objectResult = sut as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 404);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_Paging_ReturnsData()
        {
            int limit = 100;
            int page = 2;
            int total = 450;

            var request = new AttractionRequestModel { PhotoSize = "n" };
            var query = new AttractionRequest { Page = page, PageSize = limit, PhotoSize = "n" };

            var dataSet = AttractionDataSet.GetAttractionModelPagedResult(page, limit, total);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttractions(query))
                    .ReturnsAsync(dataSet);

            var controller = GetAttractionController(attractionService.Object);

            // Act
            var sut = await controller.GetList(request, limit, page);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Theory()]
        [InlineData("grid", "m")]
        [InlineData("list", "t")]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_Paging_Filter_Display(string display, string expectedPhotoSize)
        {
            int limit = 100;
            int page = 2;
            int total = 450;

            var request = new AttractionRequestModel { Display = display };
            var query = new AttractionRequest { Page = page, PageSize = limit };

            var dataSet = AttractionDataSet.GetAttractionModelPagedResult(page, limit, total);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttractions(query))
                .ReturnsAsync(dataSet);

            var controller = GetAttractionController(attractionService.Object);

            // Act
            var sut = await controller.GetList(request, limit, page);

            // Assert
            Assert.Equal(expectedPhotoSize, request.PhotoSize);
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_Paging_Filter_Radius()
        {
            int limit = 100;
            int page = 2;
            int total = 450;

            var request = new AttractionRequestModel
            {
                LocationType = new LocationType { TypeId = "radius", Latitude = 40.782222, Longitude = -73.965278, Radius = .5, },
            };

            var query = new AttractionRequest { Page = page, PageSize = limit, PhotoSize = "n" };

            var dataSet = AttractionDataSet.GetAttractionModelPagedResult(page, limit, total);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttractions(query))
                .ReturnsAsync(dataSet);

            var controller = GetAttractionController(attractionService.Object);

            // Act
            var sut = await controller.GetList(request, limit, page);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_Paging_Filter_Bounding_Box()
        {
            int limit = 100;
            int page = 2;
            int total = 450;

            var boundingBox = new BoundingBox { LowerLeftLatitude = 1, LowerLeftLongitude = 1, UpperRightLatitude = 1, UpperRightLongitude = 1 };
            var request = new AttractionRequestModel
            {
                LocationType = new LocationType { TypeId = "box", BoundingBox = boundingBox },
            };

            var query = new AttractionRequest { Page = page, PageSize = limit, PhotoSize = "n" };

            var dataSet = AttractionDataSet.GetAttractionModelPagedResult(page, limit, total);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttractions(query))
                .ReturnsAsync(dataSet);

            var controller = GetAttractionController(attractionService.Object);

            // Act
            var sut = await controller.GetList(request, limit, page);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        private AttractionController GetAttractionController(IAttractionService? attractionService = null)
        {
            attractionService = attractionService ?? new Mock<IAttractionService>().Object;
            var logger = new Mock<ILogger<AttractionController>>().Object;

            return new AttractionController(attractionService, logger);
        }
    }
}
