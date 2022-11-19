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

        public async Task<List<KeyValuePair<string, string>>> GetReferenceTypeList(string type)
        {
            var results = await _referenceRepository.GetReferenceTypeList(type);
            var models = results.Select(r => new KeyValuePair<string, string>(r.Id, r.ShortDescription)).ToList();

            return models;
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
                new() { Name = "11-Sep", Category = "History " },
                new() { Name = "Abstract", Category = "Art" },
                new() { Name = "Abstract sculpture", Category = "Art" },
                new() { Name = "African", Category = "Nationality" },
                new() { Name = "All Wars", Category = "Military" },
                new() { Name = "Allegorical", Category = "Art" },
                new() { Name = "American", Category = "Nationality" },
                new() { Name = "Animal figure", Category = "Art" },
                new() { Name = "Animal: equestrian", Category = "Art" },
                new() { Name = "Animal: other", Category = "Art" },
                new() { Name = "Arch", Category = "Architecture" },
                new() { Name = "Armillary sphere", Category = "Architecture" },
                new() { Name = "Artist", Category = "Occupation" },
                new() { Name = "Asian", Category = "Nationality" },
                new() { Name = "Athlete", Category = "Occupation" },
                new() { Name = "Bas relief", Category = "Art" },
                new() { Name = "Building/Structure", Category = "Architecture" },
                new() { Name = "Business Leader", Category = "Occupation" },
                new() { Name = "Bust", Category = "Art" },
                new() { Name = "Cairn", Category = "Art" },
                new() { Name = "Cannon/Gun", Category = "Military" },
                new() { Name = "Christian", Category = "Nationality" },
                new() { Name = "Civic Leader", Category = "Occupation" },
                new() { Name = "Civil War", Category = "Military" },
                new() { Name = "Clock", Category = "Architecture" },
                new() { Name = "Colonial", Category = "History" },
                new() { Name = "Column", Category = "Architecture" },
                new() { Name = "Composer/Musician", Category = "Occupation" },
                new() { Name = "Drinking fountain", Category = "Architecture" },
                new() { Name = "Dutch", Category = "Nationality" },
                new() { Name = "Eastern European", Category = "Nationality" },
                new() { Name = "Educator", Category = "Occupation" },
                new() { Name = "English", Category = "Nationality" },
                new() { Name = "Equestrian statue", Category = "Art" },
                new() { Name = "Exedra", Category = "Art" },
                new() { Name = "Explorer", Category = "Occupation" },
                new() { Name = "Figural sculpture", Category = "Art" },
                new() { Name = "Fireman", Category = "Occupation" },
                new() { Name = "Flagstaff", Category = "Architecture" },
                new() { Name = "French", Category = "Nationality" },
                new() { Name = "Gate", Category = "Architecture" },
                new() { Name = "German", Category = "Nationality" },
                new() { Name = "Greek", Category = "Nationality" },
                new() { Name = "Holocaust", Category = "History" },
                new() { Name = "Honor roll", Category = "Military" },
                new() { Name = "Immigration", Category = "History" },
                new() { Name = "Irish", Category = "Nationality" },
                new() { Name = "Italian", Category = "Nationality" },
                new() { Name = "Jewish", Category = "Nationality" },
                new() { Name = "Juvenile", Category = "Art" },
                new() { Name = "Korean War", Category = "Military" },
                new() { Name = "Labor", Category = "History" },
                new() { Name = "Lamppost", Category = "Architecture" },
                new() { Name = "Latino", Category = "Nationality" },
                new() { Name = "Literary Character", Category = "History" },
                new() { Name = "Lithuanian", Category = "Nationality" },
                new() { Name = "Map", Category = "History" },
                new() { Name = "Marker", Category = "History" },
                new() { Name = "Memorial grove", Category = "Military" },
                new() { Name = "Mosaic", Category = "Art" },
                new() { Name = "Mural", Category = "Art" },
                new() { Name = "Native American", Category = "Nationality" },
                new() { Name = "Obelisk", Category = "Art" },
                new() { Name = "Ornamental fountain", Category = "Architecture" },
                new() { Name = "Other", Category = "Other" },
                new() { Name = "Painting", Category = "Art" },
                new() { Name = "Philanthropy", Category = "History" },
                new() { Name = "Philosophy", Category = "Art" },
                new() { Name = "Physician", Category = "Occupation" },
                new() { Name = "Plaque", Category = "Architecture" },
                new() { Name = "Plaza", Category = "Architecture" },
                new() { Name = "Policeman", Category = "Occupation" },
                new() { Name = "Polish", Category = "Nationality" },
                new() { Name = "Politcal Figure", Category = "Occupation" },
                new() { Name = "Portrait", Category = "Art" },
                new() { Name = "Portrait sculpture", Category = "Art" },
                new() { Name = "Public Servant", Category = "Occupation" },
                new() { Name = "Religious Leader", Category = "Occupation" },
                new() { Name = "Revolutionary War", Category = "Military" },
                new() { Name = "Scandinavian", Category = "Nationality" },
                new() { Name = "Scientist/Inventor", Category = "Occupation" },
                new() { Name = "Scottish", Category = "Nationality" },
                new() { Name = "Sculpture", Category = "Art" },
                new() { Name = "Soldier", Category = "Military" },
                new() { Name = "Spanish-American War", Category = "Military" },
                new() { Name = "Stair", Category = "Architecture" },
                new() { Name = "Stele", Category = "Architecture" },
                new() { Name = "Sundial", Category = "Architecture" },
                new() { Name = "Tree marker", Category = "Architecture" },
                new() { Name = "Trough/planter", Category = "Architecture" },
                new() { Name = "US President", Category = "Occupation" },
                new() { Name = "Vietnam War", Category = "Military" },
                new() { Name = "Wall", Category = "Architecture" },
                new() { Name = "War Memorial", Category = "Military" },
                new() { Name = "War of 1812", Category = "Military" },
                new() { Name = "Women", Category = "Occupation" },
                new() { Name = "World War I", Category = "Military" },
                new() { Name = "World War II", Category = "Military" },
                new() { Name = "Writer", Category = "Occupation" },
            };

            return Task.FromResult(list);
        }

        private dynamic Convert(dynamic value)
        {
            Type dataType = value.GetType();
            string destinationType = $"NavigatorAttractions.Service.Models.ReferenceTypes.{dataType.Name}Model, NavigatorAttractions.Service";

            Type modelType = Type.GetType(destinationType);
            var mappedResult = _mapper.Map(value, value.GetType(), modelType);

            return mappedResult;
        }
    }
}
