using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Results;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Test.Data;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class AttractionCommandControllerTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Update_Primary_Photo_Returns_Success()
        {
            var dataSet = AttractionDataSet.GetAttractionModel();
            var dataSetPhoto = PhotoDataSet.GetPhotoModel();
            var resultSet = new RepositoryActionResult<AttractionModel>(dataSet, ResultConstants.UpsertedStatus);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .ReturnsAsync(dataSet);
            attractionService.Setup(b => b.UpdateAttraction(dataSet))
                .ReturnsAsync(resultSet);

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhoto(It.IsAny<string>()))
                .ReturnsAsync(dataSetPhoto);

            var controller = GetAttractionCommandController(attractionService.Object, photoService.Object);

            // Act
            var sut = await controller.Put(It.IsAny<string>(), It.IsAny<string>());

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

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Update_Primary_Photo_Returns_Photo_NotFound()
        {
            var dataSet = AttractionDataSet.GetAttractionModel();

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .ReturnsAsync(dataSet);

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhoto(It.IsAny<string>()))
                .ReturnsAsync((PhotoModel)null);

            var controller = GetAttractionCommandController(attractionService.Object, photoService.Object);

            // Act
            var sut = await controller.Put(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Update_Primary_Photo_Returns_Attraction_NotFound()
        {
            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .ReturnsAsync((AttractionModel)null);

            var controller = GetAttractionCommandController(attractionService.Object, null);

            // Act
            var sut = await controller.Put(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Patch_Attraction_Returns_Success()
        {
            var dataSet = AttractionDataSet.GetAttractionModel();
            var patch = new JsonPatchDocument<AttractionModel>
            {
                
            };

            var dataSetPhoto = PhotoDataSet.GetPhotoModel();
            var resultSet = new RepositoryActionResult<AttractionModel>(dataSet, ResultConstants.UpsertedStatus);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .ReturnsAsync(dataSet);
            attractionService.Setup(b => b.UpdateAttraction(dataSet))
                .ReturnsAsync(resultSet);

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhoto(It.IsAny<string>()))
                .ReturnsAsync(dataSetPhoto);

            var controller = GetAttractionCommandController(attractionService.Object, photoService.Object);

            // Act
            var sut = await controller.Patch( patch, It.IsAny<string>());

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

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Patch_Attraction_Returns_BadRequest()
        {
            var controller = GetAttractionCommandController();

            // Act
            var sut = await controller.Patch(null, It.IsAny<string>());

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        private AttractionCommandController GetAttractionCommandController(IAttractionService? attractionService = null, IPhotoService? photoService = null)
        {
            attractionService = attractionService ?? new Mock<IAttractionService>().Object;
            photoService = photoService ?? new Mock<IPhotoService>().Object;

            return new AttractionCommandController(attractionService, photoService);
        }
    }
}
