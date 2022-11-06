using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoDB.Driver;
using Navigator.MongoRepository.Repository;
using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Data.Entities.Photos;
using NavigatorAttractions.Data.Interface;

namespace NavigatorAttractions.Data.Repository
{
    public class PhotoRepository
           : MongoRepository<Photo, string>, IPhotoRepository
    {
        public PhotoRepository(string connectionString)
            : base(connectionString)
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreIfNullConvention(true),
            };

            ConventionRegistry.Register("camel case", pack, t => true);
        }

        public async Task<bool> GetPhotoExists(string id)
        {
            Guard.ThrowIfNull(id, nameof(id));

            return await collection.Find(_ => _.Id == id).AnyAsync();
        }

        public async Task<bool> GetPhotoExists(long photoId)
        {
            Guard.ThrowIfZeroOrLess(photoId, nameof(photoId));

            return await collection.Find(_ => _.PhotoId == photoId).AnyAsync();
        }

        public async Task<Photo> GetPhoto(string id)
        {
            Guard.ThrowIfNull(id, nameof(id));

            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Photo> GetPhoto(long photoId, DateTime? lastUpdatedDate)
        {
            Guard.ThrowIfZeroOrLess(photoId, nameof(photoId));

            var builder = Builders<Photo>.Filter.Empty;
            builder = builder & Builders<Photo>.Filter.Eq("photoId", photoId);

            var result = await collection.FindAsync(builder);
            return result.FirstOrDefault();
        }

        public async Task<IList<Photo>> GetPhotos()
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<IList<Photo>> GetPhotos(string tag)
        {
            Guard.ThrowIfNull(tag, nameof(tag));

            var builder = Builders<Photo>.Filter
                .Regex("machineTags.tag", new BsonRegularExpression(tag + ".*", "i"));

            return await collection.Find(builder).ToListAsync();
        }

        public async Task<IList<Photo>> GetPhotos(string[] tags)
        {
            Guard.ThrowIfNull(tags, nameof(tags));

            var builder = Builders<Photo>.Filter.In("machineTags.tag", tags);

            return await collection.Find(builder).ToListAsync();
        }

        //public Task<IEnumerable<Photo>> GetPhotos(PhotoRequest request, out long totalRecordsCount)
        //{
        //    Guard.ThrowIfNull(request, nameof(request));

        //    var builder = Builders<Photo>.Filter.In("machineTags.tag", request.Tags);

        //    var sortBuilder = Builders<Photo>.Sort;

        //    var sortColumn = !string.IsNullOrWhiteSpace(request.SortColumn.Trim()) ? request.SortColumn.Trim() : "title";
        //    var sort = request.SortOrder != null && request.SortOrder == "desc" ?
        //                        sortBuilder.Descending(sortColumn) : sortBuilder.Ascending(sortColumn);

        //    var result = collection.Find(builder).Sort(sort).ToListAsync().Result;
        //    totalRecordsCount = result.Count;

        //    int skippedCount = (request.Page - 1) * request.PageSize;
        //    var pagedList = result.Skip(skippedCount).Take(request.PageSize);

        //    return Task.FromResult(pagedList);
        //}

        //public async Task<RepositoryActionResult<Photo>> Upsert(Photo item)
        //{
        //    var photo = await collection.Find(x => x.PhotoId == item.PhotoId).FirstOrDefaultAsync();
        //    item.Id = photo == null ? ObjectId.GenerateNewId().ToString() : photo.Id;

        //    var filter = Builders<Photo>.Filter.Eq(x => x.Id, item.Id);
        //    var options = new FindOneAndReplaceOptions<Photo, Photo>
        //    {
        //        IsUpsert = true,
        //        ReturnDocument = ReturnDocument.After,
        //    };

        //    var result = await collection.FindOneAndReplaceAsync(filter, item, options);
        //    return new RepositoryActionResult<Photo>(result);
        //}

        public void SavePhoto(Photo photo)
        {
            if (photo == null)
                throw new ArgumentNullException(nameof(photo));

            throw new NotImplementedException();
        }
    }
}
