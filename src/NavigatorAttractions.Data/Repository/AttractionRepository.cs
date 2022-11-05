using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Navigator.MongoRepository.Repository;
using NavigatorAttractions.Data.Entities;
using NavigatorAttractions.Data.Interface;

namespace NavigatorAttractions.Data.Repository
{
    public class AttractionRepository : MongoRepository<Attraction, string>, IAttractionRepository, IDisposable
    {
        public AttractionRepository(string connectionString)
            : base(connectionString)
        {
            var pack = new ConventionPack
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreIfNullConvention(true),
                };

            ConventionRegistry.Register("camel case", pack, t => true);
        }

        public async Task<Attraction> Get(string id)
        {
            //Guard.ThrowIfNull(id, nameof(id));

            return await collection.Find(_ => _.Id == id).SingleAsync();
        }

        //    public async Task<long> GetAttractionsCount(AttractionRequest request)
        //    {
        //        var builder = GetAttractionFilter(request);
        //        return await collection.CountDocumentsAsync(builder);
        //    }

        //    public async Task<IEnumerable<Attraction>> GetAttractions(AttractionRequest request)
        //    {
        //        Guard.ThrowIfNull(request, nameof(request));

        //        var builder = GetAttractionFilter(request);

        //        var sortBuilder = Builders<Attraction>.Sort;
        //        var sortColumn = request.SortColumn != null && !string.IsNullOrWhiteSpace(request.SortColumn.Trim()) ? request.SortColumn.Trim() : "title";

        //        var sort = request.SortOrder != null && request.SortOrder == "desc" ?
        //            sortBuilder.Descending(sortColumn) : sortBuilder.Ascending(sortColumn);

        //        int skippedCount = (request.Page - 1) * request.PageSize;

        //        return await collection.Find(builder)
        //            .Sort(sort).Skip(skippedCount).Limit(request.PageSize).ToListAsync();
        //    }

        //public async Task<IEnumerable<Attraction>> GetAttractions(AttractionRequest request)
        //{
        //    Guard.ThrowIfNull(request, nameof(request));

        //    var builder = GetAttractionFilter(request);

        //    var sortBuilder = Builders<Attraction>.Sort;
        //    var sortColumn = request.SortColumn != null && !string.IsNullOrWhiteSpace(request.SortColumn.Trim()) ? request.SortColumn.Trim() : "title";

        //    var sort = request.SortOrder != null && request.SortOrder == "desc" ?
        //        sortBuilder.Descending(sortColumn) : sortBuilder.Ascending(sortColumn);

        //    int skippedCount = (request.Page - 1) * request.PageSize;

        //    return await collection.Find(builder)
        //        .Sort(sort).Skip(skippedCount).Limit(request.PageSize).ToListAsync();
        //}

        //public async Task<long> GetAttractionsCount(AttractionRequest request)
        //{
        //    var builder = GetAttractionFilter(request);
        //    return await collection.CountDocumentsAsync(builder);
        //}

        //private FilterDefinition<Attraction> GetAttractionFilter(AttractionRequest request)
        //{
        //    var builder = Builders<Attraction>.Filter.Empty;

        //    if (request.IdList != null)
        //    {
        //        var bsonIdList = request.IdList.Select(m => BsonValue.Create(ObjectId.Parse(m)));
        //        builder &= Builders<Attraction>.Filter.In("_id", bsonIdList);
        //    }

        //    return builder;
        //}





        //    private FilterDefinition<Attraction> GetAttractionFilter(AttractionRequest request)
        //    {
        //        var builder = Builders<Attraction>.Filter.Empty;

        //        if (request.IdList != null)
        //        {
        //            var bsonIdList = request.IdList.Select(m => BsonValue.Create(ObjectId.Parse(m)));
        //            builder &= Builders<Attraction>.Filter.In("_id", bsonIdList);
        //        }

        //        return builder;
        //    }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
