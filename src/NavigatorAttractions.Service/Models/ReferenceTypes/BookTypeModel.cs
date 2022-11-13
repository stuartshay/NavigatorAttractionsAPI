namespace NavigatorAttractions.Service.Models.ReferenceTypes
{
    public class BookTypeModel : ReferenceTypeModel
    {
        public string ISBN13 { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int? Pages { get; set; }

        public string Publisher { get; set; }

        public string PublishedDate { get; set; }
    }
}