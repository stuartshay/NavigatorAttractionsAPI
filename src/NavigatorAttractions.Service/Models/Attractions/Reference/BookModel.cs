using NavigatorAttractions.Service.Attributes;

namespace NavigatorAttractions.Service.Models.Attractions.Reference
{
    public class BookModel : ReferenceModel
    {
        public int Copyright { get; set; }

        [Reference("Page", "textBox")]
        public string? Page { get; set; }

        [Reference("Code Number", "textBox")]
        public string? CodeNumber { get; set; }

        public override string Type => "Book";
    }
}
