using NavigatorAttractions.Data.Filters.GeoRequest;

namespace NavigatorAttractions.Data.Filters
{
    public class AttractionRequest : RequestBase
    {
        public string? Catalog { get; set; }

        public string? Feature { get; set; }

        public string? ObjectType { get; set; }

        public string? Search { get; set; }

        public string? PhotoSize { get; set; }

        public string? KeywordMatch { get; set; }

        public List<string>? KeywordList { get; set; }

        public List<string>? IdList { get; set; }

        public string? ReferenceId { get; set; }

        public GeoWithin? Location { get; set; }
    }
}