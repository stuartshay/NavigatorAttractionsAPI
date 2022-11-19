using System.Collections.Generic;
using System.Linq;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Test.Data;
using Xunit;

namespace NavigatorAttractions.Service.Test.Mappings
{
    public class DataShapeTest
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void DataShapedModel_Fields_IsValid()
        {
            // Arrange
            int count = 5;
            var dataSet = AttractionDataSet.GetAttractionsModel(count);
            var fieldsList = new List<string> { "Id", "Title", "Inventory" };

            // Act
            var sut = new DataShapedModel<AttractionModel>()
                .CreateDataShapedObjects(dataSet, fieldsList).ToList();

            // Assert
            Assert.NotNull(sut);
            Assert.IsType<List<dynamic>>(sut);
            Assert.Equal(count, sut.Count);

            foreach (var item in sut)
            {
                Assert.NotNull(item.Id);
                Assert.NotNull(item.Title);

                var inventory = (InventoryModel)item.Inventory;
                Assert.NotNull(inventory);
                Assert.IsType<InventoryModel>(inventory);
            }
        }
    }
}
