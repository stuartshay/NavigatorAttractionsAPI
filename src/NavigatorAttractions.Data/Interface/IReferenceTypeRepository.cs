using NavigatorAttractions.Data.Entities.ReferenceTypes;

namespace NavigatorAttractions.Data.Interface
{
    public interface IReferenceTypeRepository
    {
        Task<ReferenceType> GetReferenceType(string id);

        Task<List<ReferenceType>> GetReferenceTypeList(string type);
    }
}
