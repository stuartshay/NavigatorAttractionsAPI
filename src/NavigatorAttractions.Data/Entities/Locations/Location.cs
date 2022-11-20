using MongoDB.Bson.Serialization.Attributes;

namespace NavigatorAttractions.Data.Entities.Locations
{
    [BsonIgnoreExtraElements(true)]
    public class loc
    {
        public double? lat { get; set; }

        public double? lon { get; set; }

        [BsonElement("location")]
        public string? Location { get; set; }

        [BsonElement("sectorId")]
        public string? SectorId { get; set; }

        [BsonElement("neighborhood")]
        public string? Neighborhood { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        [BsonElement("city")]
        public string? City { get; set; }

        [BsonElement("state")]
        public string? State { get; set; }

        [BsonElement("postalCode")]
        public string? PostalCode { get; set; }
    }
}
