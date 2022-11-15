using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavigatorAttractions.Service.Test.Data;
using Xunit.Abstractions;
using Xunit;

namespace NavigatorAttractions.Service.Test.ValidationRules
{
    public class PhotoRuleTest
    {
        private readonly PhotoRule _validator;

        private readonly ITestOutputHelper _output;

        public PhotoRuleTest(ITestOutputHelper output)
        {
            _validator = new PhotoRule();
            _output = output;
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Have_Validation_Error_When_Empty_Properties()
        {
            //_validator.ShouldHaveValidationErrorFor(x => x.MachineTags, (List<MachineTag>)null);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Have_Valid_Properties()
        {
            //_validator.ShouldNotHaveValidationErrorFor(x => x.Id, Guid.NewGuid().ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Not_Have_Validation_Errors_When_ValidModel_Is_Supplied()
        {
            var item = new PhotoModel
            {
                PhotoId = 1,
                LastUpdated = DateTime.Now,
                DateTaken = DateTime.Now,
                MachineTags = new List<MachineTag> { new MachineTag { Tag = "Test1" } },
                PhotoSizes = PhotoDataSet.GetPhotoSizes(false),
            };

            var validationResult = _validator.Validate(item);
            foreach (var error in validationResult.Errors)
            {
                _output.WriteLine(error.ErrorMessage);
            }

            //validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Should_Not_Have_Validation_Errors_When_ValidProperty_Is_Supplied()
        {
            var item = new PhotoModel
            {
                PhotoId = 1,
                LastUpdated = DateTime.Now,
                DateTaken = DateTime.Now,
            };

            //var validationPhotoIdResult = _validator.ValidateProperty(item, "PhotoId");
            //validationPhotoIdResult.IsValid.Should().BeTrue();

            //var validationLastUpdatedResult = _validator.ValidateProperty(item, "LastUpdated");
            //validationLastUpdatedResult.IsValid.Should().BeTrue();

            //var validationDateTakenResult = _validator.ValidateProperty(item, "DateTaken");
            //validationDateTakenResult.IsValid.Should().BeTrue();

            //var validationPropertiesResult = _validator.ValidateProperties(item, new[] { "PhotoId", "LastUpdated", "DateTaken" });
            //validationPropertiesResult.IsValid.Should().BeTrue();
        }
    }
}
