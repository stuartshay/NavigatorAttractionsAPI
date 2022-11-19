using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Attractions.Maps
{
    [CollectionName("marker")]
    public class Marker
    {
        [BsonElement("color")]
        public string? Color { get; set; }

        [BsonElement("label")]
        public string? Label { get; set; }

        [BsonElement("point")]
        public Point? Point { get; set; }
    }
}
