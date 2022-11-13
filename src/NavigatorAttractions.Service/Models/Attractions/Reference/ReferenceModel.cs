namespace NavigatorAttractions.Service.Models.Attractions.Reference
{
    public class ReferenceModel
    {
        public string? Id { get; set; }

        public string? ReferenceId { get; set; }

        public string? Name { get; set; }

        public string? Title { get; set; }

        public string? Style { get; set; }

        public virtual string? Type { get; set; }
    }
}
