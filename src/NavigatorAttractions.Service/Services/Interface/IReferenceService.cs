using NavigatorAttractions.Service.Models.Keyword;
using NavigatorAttractions.Service.Models.ReferenceTypes;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IReferenceService
    {
        Task<List<KeyValuePair<string, string>>> GetReferenceTypes();

        Task<List<KeyValuePair<string, string>>> GetReferenceTypesFormat();

        Task<ReferenceTypeModel> GetReferenceType(string id);

        Task<List<KeyValuePair<string, string>>> GetReferenceTypeList(string type);

        Task<List<KeyValuePair<string, string>>> GetCatalogs();

        Task<List<KeyValuePair<string, string>>> GetFeatures();

        Task<List<KeyValuePair<string, string>>> GetObjectTypes();

        Task<List<KeyValuePair<string, string>>> GetDisplayTypes();

        Task<List<KeywordModel>> GetKeywords();
    }
}
