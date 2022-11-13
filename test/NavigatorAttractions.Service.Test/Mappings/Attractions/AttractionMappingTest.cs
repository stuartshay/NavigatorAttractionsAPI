using AutoMapper;
using NavigatorAttractions.Service.Profiles;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Test.Data;
using Xunit;

namespace NavigatorAttractions.Service.Test.Mappings.Attractions
{
    public class AttractionMappingTest
    {
        private readonly IMapper _mapper;

        public AttractionMappingTest()
        {
            var profile = new AttractionProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(configuration);
        }

        [Fact()]
        [Trait("Category", "Unit")]
        public void Map_Attraction_To_AttractionModel_Mapping()
        {
            var item = AttractionDataSet.GetAttraction();
            var model = _mapper.Map<AttractionModel>(item);

            Assert.IsType<Attraction>(item);
            Assert.IsType<AttractionModel>(model);
            Assert.Equal(item.Id, model.Id);
        }
    }
}
