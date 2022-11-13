namespace NavigatorAttractions.Service.Models.Attractions
{
    public class InventoryModel
    {
        public string? Sculptor { get; set; }

        public string? Artist { get; set; }

        public string? Architect { get; set; }

        public string? Carver { get; set; }

        public string? Founder { get; set; }

        public string? Dimensions { get; set; }

        public string? Fabricator { get; set; }

        public string? Materials { get; set; }

        public string? Date { get; set; }

        public string? CastDate { get; set; }

        public string? Dedicated { get; set; }

        public string? Donor { get; set; }

        public string? Provenance { get; set; }

        public string? Owner { get; set; }

        public string? Style { get; set; }

        public IEnumerable<string>? Styles { get; set; }

        public string? ObjectType { get; set; }
    }
}
