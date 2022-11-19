using AutoMapper;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models.Keyword;
using NavigatorAttractions.Service.Models.ReferenceTypes;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.Service.Services
{
    public class ReferenceService : IReferenceService
    {
        private readonly IMapper _mapper;

        private readonly IReferenceTypeRepository _referenceRepository;

        public ReferenceService(IReferenceTypeRepository referenceRepository, IMapper mapper)
        {
            _referenceRepository = referenceRepository ?? throw new ArgumentNullException(nameof(referenceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<List<KeyValuePair<string, string>>> GetReferenceTypes()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new("book", "Book"),
                new("dataSource", "Data Source"),
                new("website", "Web Site"),
                new("wikipedia", "Wikipedia"),
                new("photoReference", "Photo"),
            };

            return Task.FromResult(list);
        }

        public Task<List<KeyValuePair<string, string>>> GetReferenceTypesFormat()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new("book", "blue"),
                new("dataSource", "red"),
                new("website", "green"),
                new("wikipedia", "orange"),
                new("photoReference", "purple"),
            };

            return Task.FromResult(list);
        }

        public async Task<ReferenceTypeModel> GetReferenceType(string id)
        {
            var reference = await _referenceRepository.GetReferenceType(id);
            var model = Convert(reference) as ReferenceTypeModel;

            return model;
        }

        public Task<List<KeyValuePair<string, string>>> GetReferenceTypeList(string type)
        {
            var results = _referenceRepository.GetReferenceTypeList(type).Result;
            var models = results.Select(r => new KeyValuePair<string, string>(r.Id, r.ShortDescription)).ToList();

            return Task.FromResult(models);
        }

        public Task<List<KeyValuePair<string, string>>> GetCatalogs()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new("nyccentralpark", "Central Park"),
                new("nycprospectpark", "Prospect Park"),
                new("nycparks", "NYC Parks"),
                new("nycwayfinding", "NYC Wayfinding"),
            };

            return Task.FromResult(list);
        }

        public Task<List<KeyValuePair<string, string>>> GetFeatures()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new("archbridge", "Arch Bridge"),
                new("architecture", "Architecture"),
                new("fountain", "Fountain"),
                new("garden", "Garden"),
                new("gate", "Gate"),
                new("greenwood", "GreenWood"),
                new("landscapes", "Landscape"),
                new("monument", "Monument"),
                new("nationallandmark", "National Historic Landmarks"),
                new("nationalplace", "National Historic Places"),
                new("nationaldistict", "National Historic District"),
                new("interestpoint", "Point of Interest"),
                new("recreation", "Recreation "),
                new("sculpture", "Sculpture"),
                new("streetfurniture", "Street Furniture"),
                new("tablet", "Tablet"),
            };

            return Task.FromResult(list);
        }

        public Task<List<KeyValuePair<string, string>>> GetObjectTypes()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new("MONUMENT", "Monument"),
                new("SCULPTURE", "Sculpture"),
                new("BUILDING", "Building"),
                new("STRUCTURE", "Structure"),
                new("DISTRICT", "District"),
                new("SITE", "Site"),
            };

            return Task.FromResult(list);
        }

        public Task<List<KeyValuePair<string, string>>> GetDisplayTypes()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new("permanent", "Permanent"),
                new("temporary", "Temporary"),
                new("storage", "Storage"),
                new("lost", "Lost"),
            };

            return Task.FromResult(list);
        }

        public Task<List<KeywordModel>> GetKeywords()
        {
            var list = new List<KeywordModel>
            {
                new KeywordModel { Name = "11-Sep", Category = "History " },
                new KeywordModel { Name = "Abstract", Category = "Art" },
                new KeywordModel { Name = "Abstract sculpture", Category = "Art" },
                new KeywordModel { Name = "African", Category = "Nationality" },
                new KeywordModel { Name = "All Wars", Category = "Military" },
                new KeywordModel { Name = "Allegorical", Category = "Art" },
                new KeywordModel { Name = "American", Category = "Nationality" },
                new KeywordModel { Name = "Animal figure", Category = "Art" },
                new KeywordModel { Name = "Animal: equestrian", Category = "Art" },
                new KeywordModel { Name = "Animal: other", Category = "Art" },
                new KeywordModel { Name = "Arch", Category = "Architecture" },
                new KeywordModel { Name = "Armillary sphere", Category = "Architecture" },
                new KeywordModel { Name = "Artist", Category = "Occupation" },
                new KeywordModel { Name = "Asian", Category = "Nationality" },
                new KeywordModel { Name = "Athlete", Category = "Occupation" },
                new KeywordModel { Name = "Bas relief", Category = "Art" },
                new KeywordModel { Name = "Building/Structure", Category = "Architecture" },
                new KeywordModel { Name = "Business Leader", Category = "Occupation" },
                new KeywordModel { Name = "Bust", Category = "Art" },
                new KeywordModel { Name = "Cairn", Category = "Art" },
                new KeywordModel { Name = "Cannon/Gun", Category = "Military" },
                new KeywordModel { Name = "Christian", Category = "Nationality" },
                new KeywordModel { Name = "Civic Leader", Category = "Occupation" },
                new KeywordModel { Name = "Civil War", Category = "Military" },
                new KeywordModel { Name = "Clock", Category = "Architecture" },
                new KeywordModel { Name = "Colonial", Category = "History" },
                new KeywordModel { Name = "Column", Category = "Architecture" },
                new KeywordModel { Name = "Composer/Musician", Category = "Occupation" },
                new KeywordModel { Name = "Drinking fountain", Category = "Architecture" },
                new KeywordModel { Name = "Dutch", Category = "Nationality" },
                new KeywordModel { Name = "Eastern European", Category = "Nationality" },
                new KeywordModel { Name = "Educator", Category = "Occupation" },
                new KeywordModel { Name = "English", Category = "Nationality" },
                new KeywordModel { Name = "Equestrian statue", Category = "Art" },
                new KeywordModel { Name = "Exedra", Category = "Art" },
                new KeywordModel { Name = "Explorer", Category = "Occupation" },
                new KeywordModel { Name = "Figural sculpture", Category = "Art" },
                new KeywordModel { Name = "Fireman", Category = "Occupation" },
                new KeywordModel { Name = "Flagstaff", Category = "Architecture" },
                new KeywordModel { Name = "French", Category = "Nationality" },
                new KeywordModel { Name = "Gate", Category = "Architecture" },
                new KeywordModel { Name = "German", Category = "Nationality" },
                new KeywordModel { Name = "Greek", Category = "Nationality" },
                new KeywordModel { Name = "Holocaust", Category = "History" },
                new KeywordModel { Name = "Honor roll", Category = "Military" },
                new KeywordModel { Name = "Immigration", Category = "History" },
                new KeywordModel { Name = "Irish", Category = "Nationality" },
                new KeywordModel { Name = "Italian", Category = "Nationality" },
                new KeywordModel { Name = "Jewish", Category = "Nationality" },
                new KeywordModel { Name = "Juvenile", Category = "Art" },
                new KeywordModel { Name = "Korean War", Category = "Military" },
                new KeywordModel { Name = "Labor", Category = "History" },
                new KeywordModel { Name = "Lamppost", Category = "Architecture" },
                new KeywordModel { Name = "Latino", Category = "Nationality" },
                new KeywordModel { Name = "Literary Character", Category = "History" },
                new KeywordModel { Name = "Lithuanian", Category = "Nationality" },
                new KeywordModel { Name = "Map", Category = "History" },
                new KeywordModel { Name = "Marker", Category = "History" },
                new KeywordModel { Name = "Memorial grove", Category = "Military" },
                new KeywordModel { Name = "Mosaic", Category = "Art" },
                new KeywordModel { Name = "Mural", Category = "Art" },
                new KeywordModel { Name = "Native American", Category = "Nationality" },
                new KeywordModel { Name = "Obelisk", Category = "Art" },
                new KeywordModel { Name = "Ornamental fountain", Category = "Architecture" },
                new KeywordModel { Name = "Other", Category = "Other" },
                new KeywordModel { Name = "Painting", Category = "Art" },
                new KeywordModel { Name = "Philanthropy", Category = "History" },
                new KeywordModel { Name = "Philosophy", Category = "Art" },
                new KeywordModel { Name = "Physician", Category = "Occupation" },
                new KeywordModel { Name = "Plaque", Category = "Architecture" },
                new KeywordModel { Name = "Plaza", Category = "Architecture" },
                new KeywordModel { Name = "Policeman", Category = "Occupation" },
                new KeywordModel { Name = "Polish", Category = "Nationality" },
                new KeywordModel { Name = "Politcal Figure", Category = "Occupation" },
                new KeywordModel { Name = "Portrait", Category = "Art" },
                new KeywordModel { Name = "Portrait sculpture", Category = "Art" },
                new KeywordModel { Name = "Public Servant", Category = "Occupation" },
                new KeywordModel { Name = "Religious Leader", Category = "Occupation" },
                new KeywordModel { Name = "Revolutionary War", Category = "Military" },
                new KeywordModel { Name = "Scandinavian", Category = "Nationality" },
                new KeywordModel { Name = "Scientist/Inventor", Category = "Occupation" },
                new KeywordModel { Name = "Scottish", Category = "Nationality" },
                new KeywordModel { Name = "Sculpture", Category = "Art" },
                new KeywordModel { Name = "Soldier", Category = "Military" },
                new KeywordModel { Name = "Spanish-American War", Category = "Military" },
                new KeywordModel { Name = "Stair", Category = "Architecture" },
                new KeywordModel { Name = "Stele", Category = "Architecture" },
                new KeywordModel { Name = "Sundial", Category = "Architecture" },
                new KeywordModel { Name = "Tree marker", Category = "Architecture" },
                new KeywordModel { Name = "Trough/planter", Category = "Architecture" },
                new KeywordModel { Name = "US President", Category = "Occupation" },
                new KeywordModel { Name = "Vietnam War", Category = "Military" },
                new KeywordModel { Name = "Wall", Category = "Architecture" },
                new KeywordModel { Name = "War Memorial", Category = "Military" },
                new KeywordModel { Name = "War of 1812", Category = "Military" },
                new KeywordModel { Name = "Women", Category = "Occupation" },
                new KeywordModel { Name = "World War I", Category = "Military" },
                new KeywordModel { Name = "World War II", Category = "Military" },
                new KeywordModel { Name = "Writer", Category = "Occupation" },
            };

            return Task.FromResult(list);
        }

        private dynamic Convert(dynamic value)
        {
            Type dataType = value.GetType();

            string modelStrType =
                $"NavigatorAttractionsAPI.Service.Models.ReferenceTypes.{dataType.Name}Model, NavigatorAttractionsAPI.Service";

            Type modelType = Type.GetType(modelStrType);

            return _mapper.Map(value, value.GetType(), modelType);
        }
    }
}
