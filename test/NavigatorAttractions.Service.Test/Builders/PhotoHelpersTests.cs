using System.Linq;
using NavigatorAttractions.Service.Builders;
using NavigatorAttractions.Service.Constants;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Test.Data;
using Xunit;
using Xunit.Abstractions;

namespace NavigatorAttractions.Service.Test.Builders
{
    public class PhotoHelpersTests
    {
        private readonly ITestOutputHelper _output;

        public PhotoHelpersTests(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Theory]
        [InlineData("s", 75, 75)]
        [InlineData("q", 150, 150)]
        [Trait("Category", "Unit")]
        public void Transform_PhotoModel_To_PhotoGalleryModel(string photoSize, int expectedWidth, int expectedHeight)
        {
            // Arrange
            PhotoModel dataSet = PhotoDataSet.GetPhotoModel();
            _output.WriteLine($"{dataSet.PhotoId}|");
            foreach (var x in dataSet.PhotoSizes)
            {
                _output.WriteLine($"Size:{x.Suffix}");
            }

            // Act
            var sut = PhotoHelpers.BuildPhotoModel(dataSet, photoSize);
            _output.WriteLine($"Result:{sut.Width}|{sut.Width}");
            _output.WriteLine($"Result:{sut.Url}");

            // Assert
            Assert.IsType<PhotoModel>(dataSet);
            Assert.NotNull(sut);
            Assert.IsType<PhotoGalleryModel>(sut);

            Assert.Equal(expectedWidth, sut.Width);
            Assert.Equal(expectedHeight, sut.Height);
            Assert.EndsWith($"_{photoSize}.jpg", sut.Url);
        }

        [Theory]
        [InlineData("t", 100, 75)]
        [InlineData("m", 240, 180)]
        [InlineData("z", 640, 480)]
        [InlineData("c", 800, 600)]
        [InlineData("b", 1024, 768)]
        [Trait("Category", "Unit")]
        public void Transform_PhotoModel_To_PhotoGalleryModel_Ratio_Larger_Width(string photoSize, int expectedWidth, int expectedHeight)
        {
            // Arrange
            PhotoModel dataSet = PhotoDataSet.GetPhotoModel(1, false).First();
            _output.WriteLine($"{dataSet.PhotoId}|");
            foreach (var x in dataSet.PhotoSizes)
            {
                _output.WriteLine($"Size:{x.Suffix}");
            }

            // Act
            var sut = PhotoHelpers.BuildPhotoModel(dataSet, photoSize);
            _output.WriteLine($"Result:{sut.Width}|{sut.Width}");

            // Assert
            Assert.IsType<PhotoModel>(dataSet);
            Assert.NotNull(sut);
            Assert.IsType<PhotoGalleryModel>(sut);

            Assert.Equal(expectedWidth, sut.Width);
            Assert.Equal(expectedHeight, sut.Height);
            Assert.EndsWith($"_{photoSize}.jpg", sut.Url);
        }

        [Theory]
        [InlineData("t", 75, 100)]
        [InlineData("m", 180, 240)]
        [InlineData("z", 480, 640)]
        [InlineData("c", 600, 800)]
        [InlineData("b", 768, 1024)]
        [Trait("Category", "Unit")]
        public void Transform_PhotoModel_To_PhotoGalleryModel_Ratio__Larger_Height(string photoSize, int expectedWidth, int expectedHeight)
        {
            // Arrange
            PhotoModel dataSet = PhotoDataSet.GetPhotoModel(1, true).First();
            _output.WriteLine($"{dataSet.PhotoId}|");
            foreach (var x in dataSet.PhotoSizes)
            {
                _output.WriteLine($"Size:{x.Suffix}");
            }

            // Act
            var sut = PhotoHelpers.BuildPhotoModel(dataSet, photoSize);
            _output.WriteLine($"Result:{sut.Width}|{sut.Width}");

            // Assert
            Assert.IsType<PhotoModel>(dataSet);
            Assert.NotNull(sut);
            Assert.IsType<PhotoGalleryModel>(sut);

            Assert.Equal(expectedWidth, sut.Width);
            Assert.Equal(expectedHeight, sut.Height);
            Assert.EndsWith($"_{photoSize}.jpg", sut.Url);
        }

        [Theory]
        [Trait("Category", "Unit")]
        [InlineData("md", 500, 375)]
        public void Transform_PhotoModel_To_PhotoGalleryModel_Ratio_Md(string photoSize, int expectedWidth, int expectedHeight)
        {
            PhotoModel dataSet = PhotoDataSet.GetPhotoModel(1, false).First();
            _output.WriteLine($"{dataSet.PhotoId}|");
            foreach (var x in dataSet.PhotoSizes)
            {
                _output.WriteLine($"Size:{x.Suffix}");
            }

            // Act
            var sut = PhotoHelpers.BuildPhotoModel(dataSet, photoSize);

            // Assert
            Assert.IsType<PhotoModel>(dataSet);
            Assert.NotNull(sut);
            Assert.IsType<PhotoGalleryModel>(sut);

            Assert.Equal(expectedWidth, sut.Width);
            Assert.Equal(expectedHeight, sut.Height);
            Assert.EndsWith($".jpg", sut.Url);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Get_AttractionModel_Empty_Photo()
        {
            AttractionModel dataSet = AttractionDataSet.GetAttractionModel();
            dataSet.Photo = null;

            // Act
            var sut = PhotoHelpers.CheckEmptyPhoto(dataSet);

            // Assert
            Assert.IsType<AttractionModel>(dataSet);
            Assert.NotNull(dataSet.Photo);

            Assert.NotNull(sut);
            Assert.IsType<AttractionModel>(sut);

            Assert.Equal(PhotoConstants.UrlPlaceholder, sut.Photo.Url);
            Assert.Equal(PhotoConstants.IdPlaceholder.ToString(), sut.Photo.Id);
            Assert.Equal(PhotoConstants.TitlePlaceholder, sut.Photo.Title);

            Assert.Equal(PhotoConstants.WidthPlaceholder, sut.Photo.Width);
            Assert.Equal(PhotoConstants.HeightUrlPlaceholder, sut.Photo.Height);
        }
    }
}
