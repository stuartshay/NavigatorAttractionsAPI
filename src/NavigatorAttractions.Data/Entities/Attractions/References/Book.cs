using MongoDB.Bson.Serialization.Attributes;

namespace NavigatorAttractions.Data.Entities.Attractions.References
{
    [BsonIgnoreExtraElements(true)]
    public class Book : Reference
    {
        [BsonElement("copyright")]
        public int Copyright { get; set; }

        [BsonElement("page")]
        public string Page { get; set; }

        [BsonElement("codeNumber")]
        public string CodeNumber { get; set; }

        [BsonIgnore]
        public override string Type => "Book";

        public override string ToString()
        {
            return $"Title:{this.Title}|Copyright:{this.Copyright}|CodeNumber:{this.CodeNumber}";
        }
    }
}
