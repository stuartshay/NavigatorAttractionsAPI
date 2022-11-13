namespace NavigatorAttractions.Service.Models.Attractions.Reference
{
    public class WikipediaModel : ReferenceModel
    {
        public string? Url { get; set; }

        public override string Type => "Wikipedia";
    }
}
