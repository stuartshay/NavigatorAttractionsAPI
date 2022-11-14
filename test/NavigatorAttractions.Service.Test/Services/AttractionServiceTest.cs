using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Services;
using NavigatorAttractions.Service.Test.Data;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace NavigatorAttractions.Service.Test.Services
{
    public class AttractionServiceTest
    {
        private readonly ITestOutputHelper _output;
        
        public AttractionServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction()
        {
            // Arrange 
            var dataSet = AttractionDataSet.GetAttraction();

            var mockAttractionRepository = new Mock<IAttractionRepository>();
            mockAttractionRepository.Setup(a => a.Get(It.IsAny<string>())).ReturnsAsync(dataSet);

            var service = GetAttractionService(mockAttractionRepository.Object);

            // Act
            var sut = await service.GetAttraction(dataSet.Id);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<AttractionModel>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions()
        {
            // Arrange 
            var dataSet = AttractionDataSet.GetAttractions(100);

            var mockAttractionRepository = new Mock<IAttractionRepository>();
            mockAttractionRepository.Setup(a => a.GetAttractions())
                .ReturnsAsync(dataSet);

            var service = GetAttractionService(mockAttractionRepository.Object);

            // Act
            var sut = await service.GetAttractions();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<AttractionModel>>(sut);
        }


        [Fact(Skip = "TODO")]
        [Trait("Category", "Unit")]
        public async Task Get_MachineKeys()
        {
            // Arrange 
            var dataSet = MachineTagDataSet.GetMachineTags(100);

            var mockAttractionRepository = new Mock<IAttractionRepository>();
            mockAttractionRepository.Setup(a => a.GetMachineKeys())
             //   .ReturnsAsync(dataSet)
                ;

            var service = GetAttractionService(mockAttractionRepository.Object);

            // Act
            var sut = await service.GetMachineKeys();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<string>>(sut);
        }


        private AttractionService GetAttractionService(IAttractionRepository? attractionRepository = null)
        {
            attractionRepository = attractionRepository ?? new Mock<IAttractionRepository>().Object;

            var profile = new AttractionProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var logger = new Mock<ILogger<AttractionService>>().Object;

            return new AttractionService(attractionRepository, mapper, logger);
        }
    }
}
