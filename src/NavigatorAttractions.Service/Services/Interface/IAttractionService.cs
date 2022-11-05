using NavigatorAttractions.Service.Models;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IAttractionService
    {
        Task<AttractionModel> GetAttraction(string id);
    }
}
