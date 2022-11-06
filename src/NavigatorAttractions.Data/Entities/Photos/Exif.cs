using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("exif")]
    [BsonIgnoreExtraElements(true)]
    public class Exif
    {
        public string Camera { get; set; }

        public double? ShutterSpeed { get; set; }

        public double? Aperture { get; set; }

        public double? FocalLength { get; set; }
    }
}
