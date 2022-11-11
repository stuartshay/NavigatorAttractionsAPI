namespace NavigatorAttractions.Service.Models.Attractions.Reference
{
    public class DataSourceModel : ReferenceModel
    {
        public string ControlNumber { get; set; }

        public override string Type => "DataSource";
    }
}
