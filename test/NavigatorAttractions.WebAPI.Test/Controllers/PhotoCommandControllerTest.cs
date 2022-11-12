using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Data.Results;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Results;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Test.Data;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class PhotoCommandControllerTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Save_Photo_Returns_Success()
        {
            var dataSet = PhotoDataSet.GetPhotoModel();
            var resultSet = new EntityResultModel<PhotoModel>();

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.SaveAsync(dataSet))
                 .ReturnsAsync(resultSet);

            var controller = GetPhotoCommandController(photoService.Object);

            // Act
            var sut = await controller.Post(dataSet);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Save_Photo_Returns_Bad_Request()
        {
            var controller = GetPhotoCommandController();

            // Act
            var sut = await controller.Post(null);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Save_Photo_Returns_Validation_Error()
        {
            var dataSet = PhotoDataSet.GetPhotoModel();
            dataSet.MachineTags = null;

            var resultSet = new EntityResultModel<PhotoModel>();

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.SaveAsync(dataSet))
                .ReturnsAsync(resultSet);

            var controller = GetPhotoCommandController(photoService.Object);

            // Act
            var sut = await controller.Post(dataSet);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<ObjectResult>(sut);

            var objectResult = sut as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 406);

            Assert.IsType<EntityResultModel<PhotoModel>>(objectResult.Value);
            var result = objectResult.Value as EntityResultModel<PhotoModel>;
            Assert.NotNull(result);
            Assert.Equal(ResultConstants.VaildationError, result.Status);
        }

        private PhotoCommandController GetPhotoCommandController(IPhotoService? photoService = null)
        {
            photoService = photoService ?? new Mock<IPhotoService>().Object;
            var logger = new Mock<ILogger<PhotoController>>().Object;

            return new PhotoCommandController(photoService, logger);
        }
    }
}
