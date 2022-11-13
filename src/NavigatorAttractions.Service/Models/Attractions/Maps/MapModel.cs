namespace NavigatorAttractions.Service.Models.Attractions.Maps
{
    public class MapModel
    {
        public PointModel? Center { get; set; }

        public int? Zoom { get; set; }

        public string? Size { get; set; }

        public string? MapType { get; set; }

        public List<MarkerModel>? Markers { get; set; }
    }
}
