using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("geo")]
    [BsonIgnoreExtraElements(true)]
    public class Geo
    {
        public double? Lat { get; set; }

        public double? Lon { get; set; }

        public long? WoeId { get; set; }
    }
}
