using NavigatorAttractions.Service.Models;
using NavigatorAttractions.Service.Models.Attractions;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IAttractionService
    {
        Task<AttractionModel> GetAttraction(string id);
    }
}
