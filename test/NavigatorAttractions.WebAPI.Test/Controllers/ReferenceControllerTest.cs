using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NavigatorAttractions.Service.Models.Keyword;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Test.Data;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class ReferenceControllerTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Item_Returns_Data()
        {
            var dataSet = ReferenceTypeDataSet.GetTypeModel();
            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetReferenceType(It.IsAny<string>()))
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.Get("valid");

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Item_Not_Found()
        {
            var referenceService = new Mock<IReferenceService>();

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.Get("invalid");

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<NotFoundResult>(sut);

            var objectResult = sut as NotFoundResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 404);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Item_BadRequest()
        {
            // Arrange
            var controller = GetReferenceController();

            // Act
            var sut = await controller.Get(null);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Types_Returns_Data()
        {
            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetReferenceTypes())
                          .ReturnsAsync(ReferenceTypeDataSet.GetReferenceTypes);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.Get();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<List<KeyValuePair<string, string>>>(objectResult.Value);

            var result = objectResult.Value as List<KeyValuePair<string, string>>;
            Assert.NotNull(result);
            Assert.Equal(ReferenceTypeDataSet.GetReferenceTypes().Count, result.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Type_List_Returns_Data()
        {
            var dataSet = ReferenceTypeDataSet.GetReferenceTypes();
            var item = dataSet.Select(d => d.Key).First();
            var itemsDataSet = ReferenceTypeDataSet.GetReferenceTypesItems();

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(r => r.GetReferenceTypes())
                .ReturnsAsync(dataSet);
            referenceService.Setup(r => r.GetReferenceTypeList(It.IsAny<string>()))
                .ReturnsAsync(itemsDataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetReferenceTypeList(item);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<List<KeyValuePair<string, string>>>(objectResult.Value);

            var result = objectResult.Value as List<KeyValuePair<string, string>>;
            Assert.NotNull(result);
            Assert.Equal(itemsDataSet.Count, result.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Type_Items_NotFound()
        {
            var dataSet = ReferenceTypeDataSet.GetReferenceTypes();
            var item = dataSet.Select(d => d.Key).First();

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(r => r.GetReferenceTypes())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetReferenceTypeList(item);

            // Assert
            referenceService.Verify(r => r.GetReferenceTypes());
            Assert.NotNull(sut);
            Assert.IsType<NotFoundResult>(sut);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Type_List_NotFound()
        {
            // Arrange
            var dataSet = ReferenceTypeDataSet.GetReferenceTypes();
            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(r => r.GetReferenceTypes())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetReferenceTypeList("invalid");

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<NotFoundObjectResult>(sut);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Reference_Type_List_BadRequest()
        {
            // Arrange
            var controller = GetReferenceController();

            // Act
            var sut = await controller.GetReferenceTypeList(null);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);

            var objectResult = sut as BadRequestResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 400);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Catalogs_Returns_Data()
        {
            var dataSet = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("nyccentralpark", "Central Park"),
                new KeyValuePair<string, string>("nycprospectpark", "Prospect Park"),
            };

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetCatalogs())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetCatalogs();

            // Assert
            referenceService.Verify(b => b.GetCatalogs());
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Features_Returns_Data()
        {
            var dataSet = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("archbridge", "Arch Bridge"),
                new KeyValuePair<string, string>("architecture", "Architecture"),
            };

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetFeatures())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetFeatures();

            // Assert
            referenceService.Verify(b => b.GetFeatures());
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Keywords_Returns_Data()
        {
            var dataSet = new List<KeywordModel>
            {
                new KeywordModel { Name = "11-Sep", Category = "History" },
                new KeywordModel { Name = "Abstract", Category = "Art" },
            };

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetKeywords())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetKeywords();

            // Assert
            referenceService.Verify(b => b.GetKeywords());
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_DisplayTypes_Returns_Data()
        {
            var dataSet = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("archbridge", "Arch Bridge"),
                new KeyValuePair<string, string>("architecture", "Architecture"),
            };

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetDisplayTypes())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetDisplayTypes();

            // Assert
            referenceService.Verify(b => b.GetDisplayTypes());
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);

            Assert.IsType<List<KeyValuePair<string, string>>>(objectResult.Value);

            var result = objectResult.Value as List<KeyValuePair<string, string>>;
            Assert.NotNull(result);
            Assert.Equal(dataSet.Count, result.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_ObjectTypes_Returns_Data()
        {
            var dataSet = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("archbridge", "Arch Bridge"),
                new KeyValuePair<string, string>("architecture", "Architecture"),
            };

            var referenceService = new Mock<IReferenceService>();
            referenceService.Setup(b => b.GetObjectTypes())
                .ReturnsAsync(dataSet);

            var controller = GetReferenceController(referenceService.Object);

            // Act
            var sut = await controller.GetObjectTypes();

            // Assert
            referenceService.Verify(b => b.GetObjectTypes());
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);

            Assert.IsType<List<KeyValuePair<string, string>>>(objectResult.Value);

            var result = objectResult.Value as List<KeyValuePair<string, string>>;
            Assert.NotNull(result);
            Assert.Equal(dataSet.Count, result.Count);
        }

        private ReferenceController GetReferenceController(IReferenceService? referenceService = null)
        {
            referenceService = referenceService ?? new Mock<IReferenceService>().Object;
            return new ReferenceController(referenceService);
        }
    }
}
