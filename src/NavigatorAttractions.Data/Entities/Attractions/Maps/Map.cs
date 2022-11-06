using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Attractions.Maps
{
    [CollectionName("map")]
    [BsonIgnoreExtraElements(true)]
    public class Map
    {
        [BsonIgnoreIfNull]
        [BsonElement("center")]
        public Point Center { get; set; }

        [BsonElement("zoom")]
        public int Zoom { get; set; }

        [BsonElement("size")]
        public string Size { get; set; }

        [BsonElement("mapType")]
        public string MapType { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("markers")]
        public List<Marker> Markers { get; set; }
    }
}
