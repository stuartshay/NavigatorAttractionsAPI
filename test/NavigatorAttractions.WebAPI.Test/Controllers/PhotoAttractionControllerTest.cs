using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Constants;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Test.Data;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class PhotoAttractionControllerTest
    {
        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_By_Photo_Id_ReturnsData()
        {
            int count = 3;
            var dataSet = new List<string> { "nycwayfinding:monument=puck" };
            var attrationDataSet = AttractionDataSet.GetAttractions(count);

            var photoId = 9999999;

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhotoMachineTags(It.IsAny<long>()))
                .Returns(Task.FromResult(dataSet));

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.ValidateMachineKey(It.IsAny<string>()))
                .ReturnsAsync(true);

            attractionService.Setup(b => b.GetAttractions(It.IsAny<string[]>()))
                .ReturnsAsync(attrationDataSet);

            var controller = GetPhotoAttractionController(attractionService.Object, photoService.Object);

            // Act
            var sut = await controller.Get(photoId);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<List<AttractionModel>>(objectResult.Value);

            var result = objectResult.Value as List<AttractionModel>;
            Assert.NotNull(result);
            Assert.Equal(count, result.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_By_Photo_Id_Attraction_NotFound()
        {
            var dataSet = new List<string> { "nycwayfinding:monument=puck" };
            var photoId = 9999999;

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhotoMachineTags(It.IsAny<long>()))
                .Returns(Task.FromResult(dataSet));

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.ValidateMachineKey(It.IsAny<string>()))
                .ReturnsAsync(true);

            attractionService.Setup(b => b.GetAttractions(It.IsAny<string[]>()))
                .Returns(Task.FromResult((List<AttractionModel>)null));

            var controller = GetPhotoAttractionController(attractionService.Object, photoService.Object);

            // Act
            var sut = await controller.Get(photoId);

            // Assert
            photoService.Verify(b => b.GetPhotoMachineTags(It.IsAny<long>()));

            Assert.NotNull(sut);
            Assert.IsType<NotFoundObjectResult>(sut);

            var objectResult = sut as NotFoundObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 404);
            Assert.IsType<string>(objectResult.Value);

            var result = objectResult.Value as string;
            Assert.NotNull(result);
            Assert.Equal(StatusMessageConstants.NotFoundAttraction, result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_By_Photo_Id_MachineTag_NotFound()
        {
            var dataSet = new List<string> { "nycwayfinding:monument=puck" };
            var photoId = 9999999;

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhotoMachineTags(It.IsAny<long>()))
                .ReturnsAsync(dataSet);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.ValidateMachineKey(It.IsAny<string>()))
                .ReturnsAsync(false);

            var controller = GetPhotoAttractionController(attractionService.Object, photoService.Object);

            // Act
            var sut = await controller.Get(photoId);

            // Assert
            photoService.Verify(b => b.GetPhotoMachineTags(It.IsAny<long>()));

            Assert.NotNull(sut);
            Assert.IsType<NotFoundObjectResult>(sut);

            var objectResult = sut as NotFoundObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 404);
            Assert.IsType<string>(objectResult.Value);

            var result = objectResult.Value as string;
            Assert.NotNull(result);
            Assert.Equal(StatusMessageConstants.NotFoundMachineTag, result);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_By_Photo_Id_BadRequest()
        {
            var controller = GetPhotoAttractionController();

            // Act
            var sut = await controller.Get(It.IsAny<long>());

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        private PhotoAttractionController GetPhotoAttractionController(IAttractionService? attractionService = null, IPhotoService? photoService = null)
        {
            attractionService ??= new Mock<IAttractionService>().Object;
            photoService ??= new Mock<IPhotoService>().Object;

            return new PhotoAttractionController(attractionService, photoService);
        }
    }
}
