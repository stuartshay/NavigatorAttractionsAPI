using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models.Keyword;
using NavigatorAttractions.Service.Models.ReferenceTypes;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Services;
using NavigatorAttractions.Service.Test.Data.Reference;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NavigatorAttractions.Service.Test.Services
{
    public class ReferenceServiceTest
    {
        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_ReferenceTypes()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetReferenceTypes();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeyValuePair<string, string>>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_ReferenceType_List()
        {
            // Arrange 
            var dataSet = ReferenceTypeDataSet.GetReferenceTypesList();
            var mockReferenceTypeRepository = new Mock<IReferenceTypeRepository>();
            mockReferenceTypeRepository.Setup(p => p.GetReferenceTypeList(It.IsAny<string>()))
                .ReturnsAsync(dataSet);

            var service = GetReferenceService(mockReferenceTypeRepository.Object);

            // Act
            var sut = await service.GetReferenceTypeList(It.IsAny<string>());

            // Assert 
            Assert.NotNull(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_ReferenceTypesFormat()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetReferenceTypesFormat();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeyValuePair<string, string>>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Book_Type()
        {
            // Arrange
            var bookDataSet = ReferenceTypeDataSet.GetBookTypeReference();

            var mockReferenceTypeRepository = new Mock<IReferenceTypeRepository>();
            mockReferenceTypeRepository.Setup(p => p.GetReferenceType(It.IsAny<string>()))
                .ReturnsAsync(bookDataSet);

            var service = GetReferenceService(mockReferenceTypeRepository.Object);

            // Act
            var sut = await service.GetReferenceType(It.IsAny<string>());

            //Assert
            Assert.NotNull(sut);
            Assert.IsType<BookTypeModel>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Catalogs()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetCatalogs();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeyValuePair<string, string>>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_Features()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetFeatures();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeyValuePair<string, string>>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_ObjectTypes()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetObjectTypes();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeyValuePair<string, string>>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_DisplayTypes()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetDisplayTypes();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeyValuePair<string, string>>>(sut);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public async Task Get_GetKeywords()
        {
            // Arrange 
            var service = GetReferenceService();

            // Act
            var sut = await service.GetKeywords();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<KeywordModel>> (sut);
        }

        private ReferenceService GetReferenceService(IReferenceTypeRepository? referenceRepository = null)
        {
            referenceRepository ??= new Mock<IReferenceTypeRepository>().Object;

            var profile = new ReferenceTypesProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            var logger = new Mock<ILogger<ReferenceService>>().Object;

            return new ReferenceService(referenceRepository, mapper);
        }
    }
}
