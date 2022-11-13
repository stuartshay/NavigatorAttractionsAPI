using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using NavigatorAttractions.Service.Models.MachineKeys;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Controllers;
using NavigatorAttractions.WebAPI.Test.Data;
using NavigatorAttractions.WebAPI.Test.Helpers;
using Xunit;

namespace NavigatorAttractions.WebAPI.Test.Controllers
{
    public class MachineKeyControllerTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_MachineKeys_ReturnsData()
        {
            var dataSet = MachineKeyDataSet.GetMachineKey(100);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetMachineKeys())
                .ReturnsAsync(dataSet);

            var controller = GetMachineKeyController(attractionService.Object);
            controller.ControllerContext = WebTestHelpers.GetHttpContext();

            // Act
            var sut = await controller.Get();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsAssignableFrom<IList<string>>(objectResult.Value);

            var result = objectResult.Value as List<string>;
            Assert.NotNull(result);
            Assert.Equal(dataSet.Count, result.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Predicates_ReturnsData()
        {
            var dataSet = MachineKeyDataSet.GetMachineKey(100);

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.GetPredicates())
                .ReturnsAsync(dataSet);

            var controller = GetMachineKeyController(attractionService.Object);
            controller.ControllerContext = WebTestHelpers.GetHttpContext();

            // Act
            var sut = await controller.GetPredicates();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsAssignableFrom<IList<string>>(objectResult.Value);

            var result = objectResult.Value as List<string>;
            Assert.NotNull(result);
            Assert.Equal(dataSet.Count, result.Count);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_MachineKey_Validation_Returns_Data()
        {
            // Arrange
            var dataSet = MachineKeyDataSet.GetMachineTagModel(100);
            var resultDataSet = dataSet.ToArray();

            var attractionService = new Mock<IAttractionService>();
            attractionService.Setup(b => b.ValidateMachineKey("validstring"))
                .ReturnsAsync(true);

            var controller = GetMachineKeyController(attractionService.Object);
            controller.ControllerContext = WebTestHelpers.GetHttpContext();

            // Act
            var sut = await controller.GetKeysValidation(new string[] { "Test1", "Test2" });

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<OkObjectResult>(sut);

            var objectResult = sut as OkObjectResult;
            Assert.NotNull(objectResult);
            Assert.True(objectResult.StatusCode == 200);
            Assert.IsType<List<MachineKeyResultModel>>(objectResult.Value);

            var result = objectResult.Value as List<MachineKeyResultModel>;
            Assert.NotNull(result);
            // TODO: Validate True/False
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_MachineKey_Validation_BadRequest()
        {
            var controller = GetMachineKeyController();
            controller.ControllerContext = WebTestHelpers.GetHttpContext();

            // Act
            var sut = await controller.GetKeysValidation(null);

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<BadRequestResult>(sut);
        }

        private MachineKeyController GetMachineKeyController(IAttractionService attractionService = null)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();
            responseMessage.Headers.Add("x-inlinecount", "10");

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).ReturnsAsync(responseMessage).Verifiable();

            attractionService = attractionService ?? new Mock<IAttractionService>().Object;
            return new MachineKeyController(attractionService);
        }
    }
}
