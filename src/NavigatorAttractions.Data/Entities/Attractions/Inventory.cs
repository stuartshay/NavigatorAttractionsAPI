using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Attractions
{
    [BsonIgnoreExtraElements(true)]
    [CollectionName("inventory")]
    public class Inventory
    {
        [BsonElement("sculptor")]
        [BsonIgnoreIfNull(true)]
        public string Sculptor { get; set; }

        [BsonElement("artist")]
        [BsonIgnoreIfNull(true)]
        public string Artist { get; set; }

        [BsonElement("architect")]
        [BsonIgnoreIfNull(true)]
        public string Architect { get; set; }

        [BsonElement("founder")]
        [BsonIgnoreIfNull(true)]
        public string Founder { get; set; }

        [BsonElement("carver")]
        [BsonIgnoreIfNull(true)]
        public string Carver { get; set; }

        [BsonElement("materials")]
        [BsonIgnoreIfNull(true)]
        public string Materials { get; set; }

        [BsonElement("dimensions")]
        [BsonIgnoreIfNull(true)]
        public string Dimensions { get; set; }

        [BsonElement("fabricator")]
        [BsonIgnoreIfNull(true)]
        public string Fabricator { get; set; }

        [BsonElement("date")]
        [BsonIgnoreIfNull(true)]
        public string Date { get; set; }

        [BsonElement("castDate")]
        [BsonIgnoreIfNull(true)]
        public string CastDate { get; set; }

        [BsonElement("dedicated")]
        [BsonIgnoreIfNull(true)]
        public string Dedicated { get; set; }

        [BsonElement("donor")]
        [BsonIgnoreIfNull(true)]
        public string Donor { get; set; }

        [BsonElement("provenance")]
        [BsonIgnoreIfNull(true)]
        public string Provenance { get; set; }

        [BsonElement("owner")]
        [BsonIgnoreIfNull(true)]
        public string Owner { get; set; }

        [BsonElement("style")]
        [BsonIgnoreIfNull(true)]
        public string Style { get; set; }

        [BsonElement("styles")]
        [BsonIgnoreIfNull(true)]
        public IEnumerable<string> Styles { get; set; }

        [BsonElement("objectType")]
        [BsonIgnoreIfNull(true)]
        public string ObjectType { get; set; }
    }
}
