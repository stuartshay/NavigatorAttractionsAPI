using MongoDB.Bson.Serialization.Attributes;

namespace NavigatorAttractions.Data.Entities.Attractions.References
{
    public class Website : Reference
    {
        [BsonElement("siteName")]
        public string SiteName { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonIgnore]
        public override string Type => "Website";

        public override string ToString()
        {
            return $"Title:{this.Title}|SiteName:{this.SiteName} |Url:{this.Url}";
        }
    }
}
