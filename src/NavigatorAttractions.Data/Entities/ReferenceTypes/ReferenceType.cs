using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.ReferenceTypes
{
    [CollectionName("references")]
    [BsonIgnoreExtraElements(true)]
    public class ReferenceType : IEntity<string>
    {
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        public string ShortDescription { get; set; }
    }
}
