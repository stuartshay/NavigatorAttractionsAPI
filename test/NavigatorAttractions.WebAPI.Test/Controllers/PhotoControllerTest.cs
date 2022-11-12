using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Test.Data;
using Xunit;
using Xunit.Abstractions;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class PhotoControllerTest
    {
        private readonly ITestOutputHelper _output;

        public PhotoControllerTest(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_ReturnsData()
        {
            var dataSet = PhotoDataSet.GetPhotoModel();
            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhoto("123456"))
              .ReturnsAsync(dataSet);

            var controller = GetPhotoController(photoService.Object);

            // Act
            var sut = await controller.Get("123456", "m");

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<PhotoGalleryModel>(objectResult.Value);

            var result = objectResult.Value as PhotoGalleryModel;
            Assert.NotNull(result);
            Assert.Equal(dataSet.Title, result.Title);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_Returns_NotFound()
        {
            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.GetPhoto(It.IsAny<string>()))
                .Returns(Task.FromResult((PhotoModel)null));

            var controller = GetPhotoController(photoService.Object);

            // Act
            var sut = await controller.Get(StringHelper.RandomString(10), "m");

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<NotFoundResult>(sut);

            var objectResult = sut as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 404);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_Returns_BadRequest()
        {
            var controller = GetPhotoController();

            // Act
            var sut = await controller.Get(It.IsAny<string>(), "m");

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_Paging_ReturnsData()
        {
            int limit = 100;
            int page = 2;
            int total = 450;
            var attractionDataSet = AttractionDataSet.GetAttractionModel();
            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .ReturnsAsync(attractionDataSet);

            //var photoDataSet = PhotoDataSet.GetPhotoGalleryPagedResult(page, limit, total);
            //var photoService = new Mock<IPhotoService>();
            //photoService.Setup(b => b.GetPhotos(It.IsAny<PhotoRequest>()))
            //            .ReturnsAsync(photoDataSet);

            //var controller = GetPhotoController(photoService.Object, attractionService.Object);

            //// Act
            //var sut = await controller.Get(It.IsAny<string>(), limit, page, null);

            //// Assert
            //Assert.NotNull(sut);
            //Assert.IsType<OkObjectResult>(sut);

            //var objectResult = sut as OkObjectResult;
            //Assert.NotNull(objectResult);
            //Assert.True(objectResult.StatusCode == 200);
            //Assert.IsType<PagedResultModel<PhotoGalleryModel>>(objectResult.Value);

            //var result = objectResult.Value as PagedResultModel<PhotoGalleryModel>;
            //Assert.NotNull(result);
            //Assert.Equal(limit, result.Results.Count);
            //Assert.IsType<List<PhotoGalleryModel>>(result.Results);
            //Assert.Equal(limit * page, result.To);
            //Assert.Equal(((page - 1) * limit) + 1, result.From);
            //Assert.Equal(limit, result.Limit);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_Paging_Returns_NotFound()
        {
            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetAttraction(It.IsAny<string>()))
                .Returns(Task.FromResult((AttractionModel)null));

            var controller = GetPhotoController(null, attractionService.Object);

            // Act
            var sut = await controller.Get(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), null);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<NotFoundResult>(sut);

            var objectResult = sut as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 404);
        }

        [Theory]
        [InlineData(PhotoStatus.FOUND)]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_Status_ReturnsData(PhotoStatus status)
        {
            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.ValidatePhotoStatus(123456, (DateTime?)DateTime.Now))
                .ReturnsAsync(status);

            var controller = GetPhotoController(photoService.Object);

            // Act
            var sut = await controller.GetPhotoStatus(123456, (DateTime?)DateTime.Now);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<string>(objectResult.Value);

            var result = objectResult.Value as string;

            _output.WriteLine(result + "|" + EnumHelper.GetDescription(status));

            Assert.NotNull(result);
            Assert.Equal(EnumHelper.GetDescription(status), result);
        }

        private PhotoController GetPhotoController(IPhotoService? photoService = null, IAttractionService? attractionService = null)
        {
            attractionService = attractionService ?? new Mock<IAttractionService>().Object;
            photoService = photoService ?? new Mock<IPhotoService>().Object;
            var logger = new Mock<ILogger<PhotoController>>().Object;

            return new PhotoController(photoService, attractionService, logger);
        }
    }
}