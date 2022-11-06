using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Filters;

namespace NavigatorAttractions.Data.Interface
{
    public interface IAttractionRepository
    {
        Task<Attraction> Get(string id);

        Task<IList<Attraction>> GetAttractions();

        Task<long> GetAttractionsCount(AttractionRequest request);

        Task<IEnumerable<Attraction>> GetAttractions(AttractionRequest request);

        Task<IList<Attraction>> GetAttractions(string tag);

        Task<IList<Attraction>> GetAttractions(string[] tags);

        Task<RepositoryActionResult<Attraction>> Upsert(Attraction attraction);

        Task<bool> ValidateMachineKey(string tag);

        Task<IList<string>> GetMachineKeys();
    }
}
