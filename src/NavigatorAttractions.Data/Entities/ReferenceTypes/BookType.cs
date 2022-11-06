using MongoDB.Bson.Serialization.Attributes;

namespace NavigatorAttractions.Data.Entities.ReferenceTypes
{
    public class BookType : ReferenceType
    {
        [BsonElement("isbn13")]
        public string ISBN13 { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("pages")]
        public int? Pages { get; set; }

        [BsonElement("publisher")]
        public string Publisher { get; set; }

        [BsonElement("publishedDate")]
        public string PublishedDate { get; set; }

        public override string ToString()
        {
            return $"Id:{Id}|ShortDescription:{ShortDescription}|Title:{Title}|Author{Author}";
        }
    }
}
