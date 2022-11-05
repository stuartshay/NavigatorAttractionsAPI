using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;
using NavigatorAttractions.Data.Entities.Attractions.Maps;
using NavigatorAttractions.Data.Entities.Attractions.References;
using NavigatorAttractions.Data.Entities.Locations;

namespace NavigatorAttractions.Data.Entities.Attractions
{
    [CollectionName("attractions")]
    [BsonIgnoreExtraElements(true)]
    public class Attraction : IEntity<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("key")]
        public string Key { get; set; }

        [BsonElement("featureKey")]
        public string FeatureKey { get; set; }

        [BsonElement("catalog")]
        public string Catalog { get; set; }

        [BsonElement("objectType")]
        public string ObjectType { get; set; }

        [BsonElement("slug")]
        public string Slug { get; set; }

        [BsonElement("display")]
        public string Display { get; set; }

        [BsonElement("displayDate")]
        public DisplayDate DisplayDate { get; set; }

        public loc loc { get; set; }

        [BsonElement("keywords")]
        [BsonIgnoreIfNull(true)]
        public List<string> Keywords { get; set; }

        [BsonElement("aliases")]
        [BsonIgnoreIfNull(true)]
        public List<string> Aliases { get; set; }

        [BsonElement("inventory")]
        public Inventory Inventory { get; set; }

        [BsonElement("inscription")]
        public Inscription Inscription { get; set; }

        //[BsonElement("photo")]
        //public AttractionPhoto Photo { get; set; }

        [BsonElement("map")]
        public Map Map { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("machineTags")]
        public List<MachineTag> MachineTags { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("references")]
        public List<Reference> References { get; set; }
    }
}
