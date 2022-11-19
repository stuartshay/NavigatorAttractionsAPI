using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Data.Filters.GeoRequest;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Models.Attractions.Maps;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Services;
using NavigatorAttractions.Service.Test.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Results;
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

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_By_Tags()
        {
            // Arrange 
            var dataSet = AttractionDataSet.GetAttractions(5);
            var tags = (from t in dataSet?.FirstOrDefault()?.MachineTags select t.Tag).ToArray();

            var mockAttractionRepository = new Mock<IAttractionRepository>();
            mockAttractionRepository.Setup(a => a.GetAttractions(It.IsAny<string[]>()))
                .ReturnsAsync(dataSet);

            var service = GetAttractionService(mockAttractionRepository.Object);

            // Act
            var sut = await service.GetAttractions(tags);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<AttractionModel>>(sut);
        }

        [Fact(DisplayName = "Attractions - With Markers", Skip = "TODO")]
        [Trait("Category", "Unit")]
        public async Task Get_Attraction_With_Markers_Returns_Data()
        {
            // Arrange
            //var dataSet = AttractionDataSet.GetAttraction();
            //var dataSetAttractions = AttractionDataSet.GetAttraction(10);

            //var attractionRepository = new Mock<IAttractionRepository>();
            //attractionRepository.Setup(b => b.Get(It.IsAny<string>()))
            //    .ReturnsAsync(dataSet);

            //attractionRepository.Setup(b => b.GetAttractionsCount(It.IsAny<AttractionRequest>()))
            //    .ReturnsAsync(10);

            //attractionRepository.Setup(b => b.GetAttractions(It.IsAny<AttractionRequest>()))
            //    .ReturnsAsync(dataSetAttractions);

            //var attractionService = GetAttractionService(attractionRepository.Object);

            //// Act
            //var sut = await attractionService.GetAttractionWithMarkers(It.IsAny<string>());

            //// Assert
            //Assert.NotNull(sut);
            //Assert.IsType<AttractionModel>(sut);

            //var mapMarkers = sut.Map.Markers;
            //Assert.IsType<List<MarkerModel>>(mapMarkers);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Validate_MachineKey_Returns_True()
        {
            // Arrange
            var attractionRepository = new Mock<IAttractionRepository>();
            attractionRepository.Setup(b => b.ValidateMachineKey(It.IsAny<string>()))
                .ReturnsAsync(true);

            var attractionService = GetAttractionService(attractionRepository.Object);

            // Act
            var sut = await attractionService.ValidateMachineKey(It.IsAny<string>());

            // Assert
            Assert.True(sut);
        }

        [Fact(DisplayName = "Attraction Paging")]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_Paging_Returns_Data()
        {
            int totalCount = 100;
            int pageSize = 20;

            // Arrange
            var dataSet = AttractionDataSet.GetAttractions(pageSize);

            var attractionRepository = new Mock<IAttractionRepository>();
            attractionRepository.Setup(b => b.GetAttractionsCount(It.IsAny<AttractionRequest>()))
                .ReturnsAsync(totalCount);

            attractionRepository.Setup(b => b.GetAttractions(It.IsAny<AttractionRequest>()))
                .ReturnsAsync(dataSet);

            var attractionService = GetAttractionService(attractionRepository.Object);

            // Act
            var request = new AttractionRequest { PageSize = pageSize, Page = 1 };
            var sut = await attractionService.GetAttractions(request);

            // Assert
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<PagedResultModel<dynamic>>(sut);
        }

        [Fact(DisplayName = "Attraction Paging - Location Distance")]
        [Trait("Category", "Unit")]
        public async Task Get_Attractions_Paging_Location_Distance_Returns_Data()
        {
            int totalCount = 100;
            int pageSize = 20;

            var locationPoint = new Point(1, 1);
            var location = new GeoWithin
            {
                CenterSphere = new CenterSphere { Center = locationPoint },
            };

            // Arrange
            var dataSet = AttractionDataSet.GetAttractions(pageSize);

            var attractionRepository = new Mock<IAttractionRepository>();
            attractionRepository.Setup(b => b.GetAttractionsCount(It.IsAny<AttractionRequest>()))
                .ReturnsAsync(totalCount);

            attractionRepository.Setup(b => b.GetAttractions(It.IsAny<AttractionRequest>()))
                .ReturnsAsync(dataSet);

            var attractionService = GetAttractionService(attractionRepository.Object);

            // Act
            var request = new AttractionRequest { PageSize = pageSize, Page = 1, Location = location };
            var sut = await attractionService.GetAttractions(request);

            // Assert
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<PagedResultModel<dynamic>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Save_Attraction_Returns_Success()
        {
            // Arrange
            var dataSetInputModel = AttractionDataSet.GetAttractionModel();
            var dataSetResult = new RepositoryActionResult<Attraction>(AttractionDataSet.GetAttraction(), 
                ResultConstants.UpsertedStatus);

            var attractionRepository = new Mock<IAttractionRepository>();
            attractionRepository.Setup(b => b.Upsert(It.IsAny<Attraction>()))
                .ReturnsAsync(dataSetResult);

            var attractionService = GetAttractionService(attractionRepository.Object);

            // Act
            var sut = await attractionService.UpdateAttraction(dataSetInputModel);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<RepositoryActionResult<AttractionModel>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_MachineKeys()
        {
            // Arrange 
            var dataSet = MachineTagDataSet.GetMachineTags(100).Select(s => s.Tag).ToList();

            var mockAttractionRepository = new Mock<IAttractionRepository>();
            mockAttractionRepository.Setup(a => a.GetMachineKeys())
             .ReturnsAsync(dataSet);

            var service = GetAttractionService(mockAttractionRepository.Object);

            // Act
            var sut = await service.GetMachineKeys();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<string>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Predicates()
        {
            // Arrange 
            var dataSet = MachineTagDataSet.GetMachineTags(100).Select(s => s.Tag).ToList();

            var mockAttractionRepository = new Mock<IAttractionRepository>();
            mockAttractionRepository.Setup(a => a.GetMachineKeys())
                .ReturnsAsync(dataSet);

            var service = GetAttractionService(mockAttractionRepository.Object);

            // Act
            var sut = await service.GetPredicates();

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
