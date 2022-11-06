namespace NavigatorAttractions.Service.Models.Attractions.Maps
{
    public class PointModel
    {
        public PointModel(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
