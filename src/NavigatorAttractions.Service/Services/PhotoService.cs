using AutoMapper;
using AutoMapper.Execution;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Entities.Photos;
using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.Service.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        private readonly IMapper _mapper;

        public PhotoService(IPhotoRepository photoRepository, IMapper mapper)
        {
            _photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
            _mapper = mapper;
        }

        public async Task<bool> GetPhotoExist(string id)
        {
            bool isValid = long.TryParse(id, out _);
            if (isValid)
            {
                return await _photoRepository.GetPhotoExists(long.Parse(id));
            }

            return await _photoRepository.GetPhotoExists(id);
        }

        public async Task<PhotoModel> GetPhoto(string id)
        {
            var result = await _photoRepository.GetPhoto(id);
            return _mapper.Map<Photo, PhotoModel>(result);
        }

        public async Task<PhotoModel> GetPhoto(long photoId)
        {
            var result = await _photoRepository.GetPhoto(photoId, null);
            return _mapper.Map<Photo, PhotoModel>(result);
        }

        public async Task<List<string>> GetPhotoMachineTags(string photoId)
        {
            var id = long.Parse(photoId);
            var photo = await this.GetPhoto(id);
            var tags = photo?.MachineTags?.Select(p => p.Tag.ToLower());

            return tags?.ToList();
        }

        public async Task<PhotoModel> GetPhoto(long photoId, DateTime? lastUpdated)
        {
            var result = await _photoRepository.GetPhoto(photoId, null);
            return _mapper.Map<Photo, PhotoModel>(result);
        }

        public async Task<IEnumerable<PhotoModel>> GetPhotos()
        {
            var result = await _photoRepository.GetPhotos();
            return _mapper.Map<List<Photo>, List<PhotoModel>>(result.ToList());
        }

        //public async Task<IEnumerable<PhotoGalleryModel>> GetPhotos(string[] tags)
        //{
        //    var photos = new List<PhotoGalleryModel>();

        //    var result = await _photoRepository.GetPhotos(tags);
        //    foreach (var photo in result)
        //    {
        //        var p = new PhotoGalleryModel
        //        {
        //            PhotoId = photo.PhotoId,
        //            Title = photo.Title,
        //            Id = photo.Id,
        //            Url = (from x in photo.PhotoSizes where x.Suffix == "t" select x.Url).FirstOrDefault(),
        //            Width = (from x in photo.PhotoSizes where x.Suffix == "t" select x.Width).FirstOrDefault(),
        //            Height = (from x in photo.PhotoSizes where x.Suffix == "t" select x.Height).FirstOrDefault(),
        //        };
        //        photos.Add(p);
        //    }

        //    return photos;
        //}

        //public async Task<PagedResultModel<PhotoGalleryModel>> GetPhotos(PhotoRequest photoRequest)
        //{
        //    var results = await _photoRepository.GetPhotos(photoRequest, out var totalCount);

        //    var photos = Mapper.Map<List<Photo>, List<PhotoModel>>(results.ToList())
        //        .Select(p => (PhotoGalleryModel)PhotoHelpers.BuildPhotoModel(p, photoRequest.PhotoSize));

        //    var resultList = new PagedResultModel<PhotoGalleryModel>()
        //    {
        //        Total = totalCount,
        //        Page = photoRequest.Page,
        //        Limit = photoRequest.PageSize,
        //        Results = photos.ToList(),
        //    };

        //    return resultList;
        //}

        //public async Task<EntityResultModel<PhotoModel>> SaveAsync(PhotoModel item)
        //{
        //    var result = new EntityResultModel<PhotoModel>();

        //    var validate = await ValidatePhotoStatus(item.PhotoId, item.LastUpdated);
        //    if (validate == PhotoStatus.FOUND)
        //    {
        //        var entity = await _photoRepository.GetPhoto(item.PhotoId, null);
        //        result = new EntityResultModel<PhotoModel>
        //        {
        //            Status = validate.ToString(),
        //            Entity = Mapper.Map<Photo, PhotoModel>(entity),
        //        };
        //    }
        //    else if (validate == PhotoStatus.NOT_FOUND || validate == PhotoStatus.UPDATE)
        //    {
        //        var entity = _photoRepository.Upsert(Mapper.Map<PhotoModel, Photo>(item)).Result;
        //        result = new EntityResultModel<PhotoModel>
        //        {
        //            Status = validate.ToString(),
        //            Entity = Mapper.Map<Photo, PhotoModel>(entity.Entity),
        //        };
        //    }

        //    return result;
        //}

        public async Task<PhotoStatus> ValidatePhotoStatus(long photoId, DateTime? lastUpdated)
        {
            var status = PhotoStatus.NOT_FOUND;
            var result = await GetPhoto(photoId, null);

            if (result == null)
                status = PhotoStatus.NOT_FOUND;

            if (result != null && !lastUpdated.HasValue)
                status = PhotoStatus.UPDATE;

            if (result != null && lastUpdated.HasValue)
            {
                if (lastUpdated.Value > result.LastUpdated)
                    status = PhotoStatus.UPDATE;
                else
                    status = PhotoStatus.FOUND;
            }

            return status;
        }
    }
}
