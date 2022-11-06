using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("displayCategories")]
    [BsonIgnoreExtraElements(true)]
    public class DisplayCategory
    {
        public string Category { get; set; }
    }
}