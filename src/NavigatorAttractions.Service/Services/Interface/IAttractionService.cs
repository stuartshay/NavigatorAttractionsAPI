using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Service.Models.Attractions;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IAttractionService
    {
        Task<AttractionModel> GetAttraction(string id);

        Task<AttractionModel> GetAttractionWithMarkers(string id);

        Task<IList<AttractionModel>> GetAttractions();

        Task<List<AttractionModel>> GetAttractions(string[] tags);

        Task<PagedResultModel<dynamic>> GetAttractions(AttractionRequest attractionRequest);

        Task<RepositoryActionResult<AttractionModel>> UpdateAttraction(AttractionModel attraction);

        Task<bool> ValidateMachineKey(string key);

        Task<IList<string>> GetMachineKeys();

        Task<IList<string>> GetPredicates();
    }
}
