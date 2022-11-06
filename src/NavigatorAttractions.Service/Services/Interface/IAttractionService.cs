using NavigatorAttractions.Service.Models;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IAttractionService
    {
        Task<NavigatorAttractions.Service.Models.Attactions.AttractionModel> GetAttraction(string id);
    }
}
