using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Attractions
{
    [BsonIgnoreExtraElements(true)]
    [CollectionName("inscription")]
    public class Inscription
    {
        [BsonElement("bodyText")]
        public string BodyText { get; set; }
    }
}
