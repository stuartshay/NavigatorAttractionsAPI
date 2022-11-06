using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Navigator.MongoRepository;
using NavigatorAttractions.Data.Entities.Attractions;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("photos")]
    [BsonIgnoreExtraElements(true)]
    public class Photo : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("photoId")]
        public long PhotoId { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("dateUploaded")]
        public DateTime DateUploaded { get; set; }

        [BsonElement("dateTaken")]
        public DateTime DateTaken { get; set; }

        [BsonElement("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [BsonElement("machineTags")]
        public List<MachineTag> MachineTags { get; set; }

        [BsonElement("permission")]
        public Permission Permission { get; set; }

        [BsonElement("photoSizes")]
        public List<PhotoSize> PhotoSizes { get; set; }

        [BsonElement("author")]
        public Author Author { get; set; }

        [BsonElement("geo")]
        public Geo Geo { get; set; }

        [BsonElement("exif")]
        public Exif Exif { get; set; }

        [BsonElement("displayCategories")]
        public List<DisplayCategory> DisplayCategories { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}|Title:{Title}|PhotoId:{PhotoId}";
        }
    }
}
