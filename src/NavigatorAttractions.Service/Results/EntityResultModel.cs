namespace NavigatorAttractions.Service.Results
{
    public class EntityResultModel<T>
    {
        public dynamic Entity { get; set; }

        public string? Status { get; set; }

        public int StatusCode { get; set; }

        public string? ValidationErrors { get; set; }
    }
}
