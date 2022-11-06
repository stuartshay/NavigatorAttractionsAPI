using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Entities.Photos;
using NavigatorAttractions.Data.Filters;

namespace NavigatorAttractions.Data.Interface
{
    public interface IPhotoRepository
    {
        Task<bool> GetPhotoExists(string id);

        Task<bool> GetPhotoExists(long photoId);

        Task<Photo> GetPhoto(string id);

        Task<Photo> GetPhoto(long photoId, DateTime? lastUpdatedDate);

        Task<IList<Photo>> GetPhotos();

        Task<IList<Photo>> GetPhotos(string tag);

        Task<IList<Photo>> GetPhotos(string[] tags);

        Task<IEnumerable<Photo>> GetPhotos(PhotoRequest request, out long totalRecordsCount);

        Task<RepositoryActionResult<Photo>> Upsert(Photo item);

        void SavePhoto(Photo photo);
    }
}
