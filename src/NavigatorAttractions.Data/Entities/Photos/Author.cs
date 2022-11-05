using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("author")]
    [BsonIgnoreExtraElements(true)]
    public class Author
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }
    }
}
