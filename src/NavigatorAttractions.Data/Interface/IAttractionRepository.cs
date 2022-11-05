using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Filters;

namespace NavigatorAttractions.Data.Interface
{
    public interface IAttractionRepository
    {
        Task<Attraction> Get(string id);

        Task<long> GetAttractionsCount(AttractionRequest request);

        Task<IEnumerable<Attraction>> GetAttractions(AttractionRequest request);
    }
}
