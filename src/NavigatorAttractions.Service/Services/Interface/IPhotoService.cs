using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Results;

namespace NavigatorAttractions.Service.Services.Interface
{
    public interface IPhotoService
    {
        Task<bool> GetPhotoExist(string id);

        Task<PhotoModel> GetPhoto(string id);

        Task<PhotoModel> GetPhoto(long photoId);

        Task<List<string>> GetPhotoMachineTags(long photoId);

        Task<PhotoModel> GetPhoto(long photoId, DateTime? lastUpdated);

        Task<IEnumerable<PhotoModel>> GetPhotos();

        Task<IEnumerable<PhotoGalleryModel>> GetPhotos(string[] tags);

        Task<PagedResultModel<PhotoGalleryModel>> GetPhotos(PhotoRequest photoRequest);

        Task<EntityResultModel<PhotoModel>> SaveAsync(PhotoModel item);

        Task<PhotoStatus> ValidatePhotoStatus(long photoId, DateTime? lastUpdated);
    }
}
