using AutoMapper;
using NavigatorAttractions.Data.Entities.Attractions.References;
using NavigatorAttractions.Service.Models.Attractions.Reference;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Service.Test.Data.Reference;
using Xunit;

namespace NavigatorAttractions.Service.Test.Mappings.Attractions
{
    public class ReferenceMappingTest
    {
        private readonly IMapper _mapper;

        public ReferenceMappingTest()
        {
            var profile = new AttractionProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Map_Book_To_BookModel_Mapping()
        {
            var item = ReferenceDataSet.GetBookReference();
            var model = _mapper.Map<BookModel>(item);

            Assert.IsType<Book>(item);
            Assert.IsType<BookModel>(model);
            Assert.Equal("Book", model.Type);
            Assert.Equal($"Title:{item.Title}|Copyright:{item.Copyright}|CodeNumber:{item.CodeNumber}", item.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Map_DataSource_To_DataSourceModel_Mapping()
        {
            var item = ReferenceDataSet.GetDataSourceReference();
            var model = _mapper.Map<DataSourceModel>(item);

            Assert.IsType<DataSource>(item);
            Assert.IsType<DataSourceModel>(model);
            Assert.Equal("DataSource", model.Type);
            Assert.Equal($"Title:{item.Title}|Url:{item.ControlNumber}", item.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Map_Website_To_WebsiteModel_Mapping()
        {
            var item = ReferenceDataSet.GetWebsiteReference();
            var model = _mapper.Map<WebsiteModel>(item);

            Assert.IsType<Website>(item);
            Assert.IsType<WebsiteModel>(model);
            Assert.Equal("Website", model.Type);
            Assert.Equal($"Title:{item.Title}|SiteName:{item.SiteName} |Url:{item.Url}", item.ToString());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Map_Wikipedia_To_WikipediaModel_Mapping()
        {
            var item = ReferenceDataSet.GetWikipediaReference();
            var model = _mapper.Map<WikipediaModel>(item);

            Assert.IsType<Wikipedia>(item);
            Assert.IsType<WikipediaModel>(model);
            Assert.Equal("Wikipedia", item.Type);
            Assert.Equal("Wikipedia", model.Type);
            Assert.Equal($"Title:{item.Title}|Url:{item.Url}", item.ToString());
        }
    }
}
