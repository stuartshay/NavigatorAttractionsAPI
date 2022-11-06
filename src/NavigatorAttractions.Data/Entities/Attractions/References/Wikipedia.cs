using MongoDB.Bson.Serialization.Attributes;

namespace NavigatorAttractions.Data.Entities.Attractions.References
{
    public class Wikipedia : Reference
    {
        [BsonElement("url")]
        public string Url { get; set; }

        [BsonIgnore]
        public override string Type => "Wikipedia";

        public override string ToString()
        {
            return $"Title:{this.Title}|Url:{this.Url}";
        }
    }
}
