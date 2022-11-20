using MongoDB.Bson.Serialization.Attributes;

namespace NavigatorAttractions.Data.Entities.Attractions.References
{
    [BsonIgnoreExtraElements(true)]
    public class DataSource : Reference
    {
        [BsonElement("controlNumber")]
        public string? ControlNumber { get; set; }

        [BsonIgnore]
        public override string Type => "DataSource";

        public override string ToString()
        {
            return $"Title:{this.Title}|Url:{this.ControlNumber}";
        }
    }
}
