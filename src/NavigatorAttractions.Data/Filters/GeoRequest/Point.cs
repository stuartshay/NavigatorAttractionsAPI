namespace NavigatorAttractions.Data.Filters.GeoRequest
{
    public class Point
    {
        public Point(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
