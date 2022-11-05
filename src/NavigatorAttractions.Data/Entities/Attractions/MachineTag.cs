using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Attractions
{
    [CollectionName("machineTags")]
    public class MachineTag
    {
        [BsonElement("tag")]
        public string Tag { get; set; }
    }
}
