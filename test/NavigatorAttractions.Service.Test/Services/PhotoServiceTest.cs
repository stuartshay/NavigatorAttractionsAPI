using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Data.Entities.Photos;
using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Services;
using NavigatorAttractions.Service.Test.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NavigatorAttractions.Service.Test.Services
{
    public class PhotoServiceTest
    {
        private readonly ITestOutputHelper _output;

        public PhotoServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }


        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Photo()
        {
            // Arrange
            var dataSet = PhotoDataSet.GetPhoto();
            
            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(p => p.GetPhoto(It.IsAny<string>()))
                .ReturnsAsync(dataSet);

            var service = GetPhotoService(mockPhotoRepository.Object);

            // Act 
            var sut = await service.GetPhoto(dataSet.Id);

            // Assert
            Assert.NotNull(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_By_PhotoId()
        {
            // Arrange
            var dataSet = PhotoDataSet.GetPhoto();

            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(p => p.GetPhoto(It.IsAny<long>(), null))
                .ReturnsAsync(dataSet);

            var service = GetPhotoService(mockPhotoRepository.Object);

            // Act 
            var sut = await service.GetPhoto(It.IsAny<long>());

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_Exists_True()
        {
            // Arrange
            var photoRepository = new Mock<IPhotoRepository>();
            photoRepository.Setup(b => b.GetPhotoExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            var photoService = GetPhotoService(photoRepository.Object);

            // Act
            var sut = await photoService.GetPhotoExist(It.IsAny<string>());

            // Assert
            photoRepository.Verify(b => b.GetPhotoExists(It.IsAny<string>()));
            Assert.True(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Photo_MachineTags()
        {
            // Arrange
            var dataSet = PhotoDataSet.GetPhoto();

            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(p => p.GetPhoto(It.IsAny<long>(), null))
                .ReturnsAsync(dataSet);

            var service = GetPhotoService(mockPhotoRepository.Object);

            // Act 
            var sut = await service.GetPhotoMachineTags(It.IsAny<long>());

            // Assert
            Assert.NotNull(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Photos()
        {
            // Arrange
            var dataSet = PhotoDataSet.GetPhotoList(100);

            var mockPhotoRepository = new Mock<IPhotoRepository>();
            mockPhotoRepository.Setup(p => p.GetPhotos())
                .ReturnsAsync(dataSet);

            var service = GetPhotoService(mockPhotoRepository.Object);

            // Act 
            var sut = await service.GetPhotos();

            // Assert
            Assert.NotNull(sut);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Validate_Photo_Status_Found()
        {
            // Arrange
            var dataSet = PhotoDataSet.GetPhoto();
            dataSet.LastUpdated = DateTime.Now;

            var photoRepository = new Mock<IPhotoRepository>();
            photoRepository.Setup(b => b.GetPhoto(123456, null))
                .ReturnsAsync(dataSet);

            var photoService = GetPhotoService(photoRepository.Object);

            // Act
            var sut = await photoService.ValidatePhotoStatus(123456, DateTime.Now.AddSeconds(-60));

            // Assert
            Assert.IsType<PhotoStatus>(sut);
            Assert.Equal(PhotoStatus.FOUND, sut);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Validate_Photo_Status_NotFound()
        {
            // Arrange
            var photoRepository = new Mock<IPhotoRepository>();
            photoRepository.Setup(b => b.GetPhoto(123456, null))
                .ReturnsAsync((Photo)null);

            var photoService = GetPhotoService(photoRepository.Object);

            // Act
            var sut = await photoService.ValidatePhotoStatus(123456, DateTime.Now);

            // Assert
            Assert.IsType<PhotoStatus>(sut);
            Assert.Equal(PhotoStatus.NOT_FOUND, sut);
        }

        [Fact(Skip = "TODO")]
        [Trait("Category", "Unit")]
        public async Task Save_Photo_Success()
        {
            // Arrange
            var dataSet = PhotoDataSet.GetPhoto();
            var dataSetModel = PhotoDataSet.GetPhotoModel();

            var photoRepository = new Mock<IPhotoRepository>();
            photoRepository.Setup(b => b.GetPhotoExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            photoRepository.Setup(b => b.GetPhoto(123456, null))
                .ReturnsAsync(dataSet);

            var photoService = GetPhotoService(photoRepository.Object);

            // Act
            var sut = await photoService.SaveAsync(dataSetModel);

            // Assert
            Assert.NotNull(sut);
        }

        private PhotoService GetPhotoService(IPhotoRepository? photoRepository = null)
        {
            photoRepository ??= new Mock<IPhotoRepository>().Object;

            var profile = new PhotoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var logger = new Mock<ILogger<PhotoService>>().Object;

            return new PhotoService(photoRepository, mapper);
        }

    }
}
