using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Service.Models.Photos;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IPhotoService
    {
        Task<bool> GetPhotoExist(string id);

        Task<PhotoModel> GetPhoto(string id);

        Task<PhotoModel> GetPhoto(long photoId);

        Task<List<string>> GetPhotoMachineTags(string photoId);

        Task<PhotoModel> GetPhoto(long photoId, DateTime? lastUpdated);

        Task<IEnumerable<PhotoModel>> GetPhotos();

        //Task<IEnumerable<PhotoGalleryModel>> GetPhotos(string[] tags);

        //Task<PagedResultModel<PhotoGalleryModel>> GetPhotos(PhotoRequest photoRequest);

        //Task<EntityResultModel<PhotoModel>> SaveAsync(PhotoModel item);

        Task<PhotoStatus> ValidatePhotoStatus(long photoId, DateTime? lastUpdated);
    }
}
