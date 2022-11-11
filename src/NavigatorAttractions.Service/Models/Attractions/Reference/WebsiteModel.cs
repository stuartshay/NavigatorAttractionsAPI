namespace NavigatorAttractions.Service.Models.Attractions.Reference
{
    public class WebsiteModel : ReferenceModel
    {
        public string SiteName { get; set; }

        public string Url { get; set; }

        public override string Type => "Website";
    }
}