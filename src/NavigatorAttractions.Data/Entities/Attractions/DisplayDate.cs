using MongoDB.Bson.Serialization.Attributes;
using Navigator.MongoRepository;

namespace NavigatorAttractions.Data.Entities.Attractions
{
    [BsonIgnoreExtraElements(true)]
    [CollectionName("displayDate")]
    public class DisplayDate
    {
        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }
    }
}
