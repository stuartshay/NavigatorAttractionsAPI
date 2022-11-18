using Microsoft.AspNetCore.Mvc;
using Moq;
using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Service.Models.PhotoCollections;
using NavigatorAttractions.Service.Results;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class PhotoCollectionControllerTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_PhotoCollection_Returns_Status()
        {
            List<PhotoCollectionRequest> request = new List<PhotoCollectionRequest>
            {
                new() { LastUpdatedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture), PhotoId = "123456" },
                new() { LastUpdatedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture), PhotoId = "123456" },
                new() { LastUpdatedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture), PhotoId = "123456" },
            };

            var photoService = new Mock<IPhotoService>();
            photoService.Setup(b => b.ValidatePhotoStatus(123456, DateTime.Now))
                .ReturnsAsync(PhotoStatus.FOUND);

            var controller = GetPhotoCollectionController(photoService.Object);

            // Act
            var sut = await controller.GetPhotoCollectionStatus(request);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);

            Assert.IsType<List<PhotoStatusResult>>(objectResult.Value);

            var result = objectResult.Value as List<PhotoStatusResult>;
            Assert.NotNull(result);
            Assert.Equal(request.Count, result.Count);
        }

        private PhotoCollectionController GetPhotoCollectionController(IPhotoService? photoService = null)
        {
            photoService ??= new Mock<IPhotoService>().Object;

            return new PhotoCollectionController(photoService);
        }
    }
}
