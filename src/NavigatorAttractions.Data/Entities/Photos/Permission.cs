using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Photos
{
    [CollectionName("permission")]
    [BsonIgnoreExtraElements(true)]
    public class Permission
    {
        public bool IsPublic { get; set; }

        public bool IsFamily { get; set; }

        public bool IsFriend { get; set; }
    }
}
