using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoDB.Driver;
using Navigator.MongoRepository.Repository;
using NavigatorAttractions.Data.Entities.ReferenceTypes;
using NavigatorAttractions.Data.Interface;
using Newtonsoft.Json;

namespace NavigatorAttractions.Data.Repository
{
    public class ReferenceTypeRepository
               : MongoRepository<ReferenceType, string>, IReferenceTypeRepository
    {
        private readonly IMongoCollection<BsonDocument> _documentCollection;

        public ReferenceTypeRepository(string connectionString)
            : base(connectionString)
        {
            var repository = new MongoRepository<ReferenceType>(connectionString);

            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreIfNullConvention(true),
            };

            ConventionRegistry.Register("camel case", pack, t => true);

            _documentCollection = repository.GetDocumentCollection(connectionString);
        }

        public Task<ReferenceType> GetReferenceType(string id)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("_id", id);

            var document = _documentCollection.FindAsync<BsonDocument>(filter).Result.FirstOrDefault();

            var model = Convert(document) as ReferenceType;
            return Task.FromResult(model);
        }

        public async Task<List<ReferenceType>> GetReferenceTypeList(string type)
        {
            var modelList = new List<ReferenceType>();

            var filter = Builders<BsonDocument>.Filter.Regex("_t", new BsonRegularExpression(type, "i"));
            using (var cursor = await _documentCollection.FindAsync<BsonDocument>(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    modelList.AddRange(batch.Select(document => Convert(document) as ReferenceType));
                }
            }

            return modelList;
        }

        private dynamic Convert(BsonDocument docValue)
        {
            string dataStrType =
                $"NavigatorAttractionsAPI.Data.Model.ReferenceTypes.{docValue["_t"]}, NavigatorAttractionsAPI.Data";

            Type dataType = Type.GetType(dataStrType);

            dynamic doc = JsonConvert.DeserializeObject(docValue.ToJson(), dataType);

            doc.Id = docValue["_id"].AsString;
            return doc;
        }
    }
}
