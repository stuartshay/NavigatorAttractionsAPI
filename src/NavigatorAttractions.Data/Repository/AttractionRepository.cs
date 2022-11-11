using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Navigator.MongoRepository.Repository;
using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Entities.Attractions;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Data.Interface;
using NavigatorAttractions.Data.Results;

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
            Guard.ThrowIfNull(id, nameof(id));

            return await collection.Find(_ => _.Id == id).SingleAsync();
        }

        public async Task<IList<Attraction>> GetAttractions()
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<long> GetAttractionsCount(AttractionRequest request)
        {
            var builder = GetAttractionFilter(request);
            return await collection.CountDocumentsAsync(builder);
        }

        public async Task<IEnumerable<Attraction>> GetAttractions(AttractionRequest request)
        {
            Guard.ThrowIfNull(request, nameof(request));

            var builder = GetAttractionFilter(request);

            var sortBuilder = Builders<Attraction>.Sort;
            var sortColumn = request.SortColumn != null && !string.IsNullOrWhiteSpace(request.SortColumn.Trim()) ? request.SortColumn.Trim() : "title";

            var sort = request.SortOrder != null && request.SortOrder == "desc" ?
                sortBuilder.Descending(sortColumn) : sortBuilder.Ascending(sortColumn);

            int skippedCount = (request.Page - 1) * request.PageSize;

            return await collection.Find(builder)
                .Sort(sort).Skip(skippedCount).Limit(request.PageSize).ToListAsync();
        }

        public async Task<IList<Attraction>> GetAttractions(string tag)
        {
            Guard.ThrowIfNull(tag, nameof(tag));

            var builder = Builders<Attraction>.Filter
                .ElemMatch(x => x.MachineTags, x => x.Tag.ToLower() == tag.ToLower());

            return await collection.Find(builder).ToListAsync();
        }

        public async Task<IList<Attraction>> GetAttractions(string[] tags)
        {
            Guard.ThrowIfNull(tags, nameof(tags));

            var builder = Builders<Attraction>.Filter.Empty;
            foreach (var tag in tags)
            {
                builder &= Builders<Attraction>.Filter
                    .ElemMatch(x => x.MachineTags, x => x.Tag.ToLower() == tag.ToLower());
            }

            return await collection.Find(builder).ToListAsync();
        }

        public async Task<RepositoryActionResult<Attraction>> Upsert(Attraction attraction)
        {
            if (attraction.Id == null)
                attraction.Id = ObjectId.GenerateNewId().ToString();

            var filter = Builders<Attraction>.Filter.Eq(x => x.Id, attraction.Id);
            var options = new FindOneAndReplaceOptions<Attraction, Attraction>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After,
            };

            var result = await collection.FindOneAndReplaceAsync(filter, attraction, options);
            return new RepositoryActionResult<Attraction>(result);
        }

        public async Task<bool> ValidateMachineKey(string tag)
        {
            Guard.ThrowIfNull(tag, nameof(tag));

            var builder = Builders<Attraction>.Filter
                .ElemMatch(x => x.MachineTags, x => x.Tag.ToLower() == tag.ToLower());

            return await collection.Find(builder).AnyAsync();
        }

        public async Task<IList<string>> GetMachineKeys()
        {
            var pipeline = new[] {
                    new BsonDocument { { "$project", new BsonDocument("tags", "$machineTags.tag") } },
                    new BsonDocument { { "$unwind", "$tags" } },
                    new BsonDocument { { "$group", new BsonDocument {
                        { "_id", "tags"},
                        { "items",  new BsonDocument
                            {
                                { "$addToSet", "$tags" }
                            }
                        } }
                    } }
                };

            var results = await collection.Aggregate<AggregationResult>(pipeline).ToListAsync();
            var result = results.FirstOrDefault().Items
                .Select(r => r.ToLower())
                .OrderBy(r => r)
                .ToList();

            return result;
        }

        private FilterDefinition<Attraction> GetAttractionFilter(AttractionRequest request)
        {
            var builder = Builders<Attraction>.Filter.Empty;

            if (request.IdList != null)
            {
                var bsonIdList = request.IdList.Select(m => BsonValue.Create(ObjectId.Parse(m)));
                builder &= Builders<Attraction>.Filter.In("_id", bsonIdList);
            }

            return builder;
        }

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
