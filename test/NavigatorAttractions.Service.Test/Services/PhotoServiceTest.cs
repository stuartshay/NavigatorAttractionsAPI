using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Services;
using System.Threading.Tasks;
using NavigatorAttractions.Service.Test.Data;
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
