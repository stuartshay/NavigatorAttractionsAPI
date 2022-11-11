namespace NavigatorAttractions.WebAPI.Filters
{
    /// <summary>
    /// Location Filtering
    /// </summary>
    public class LocationType
    {
        /// <summary>
        /// TypeId - Radius,Box.
        /// </summary>
        public string? TypeId { get; set; }

        /// <summary>
        /// Radius Center Latitude.
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Radius Center Longitude.
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Radius (Radians)
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        ///  Bounding Box.
        /// </summary>
        public BoundingBox? BoundingBox { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// Lower Left Latitude
        /// </summary>
        public double? LowerLeftLatitude { get; set; }

        /// <summary>
        /// Lower Left Longitude
        /// </summary>
        public double? LowerLeftLongitude { get; set; }

        /// <summary>
        /// Upper Right Latitude.
        /// </summary>
        public double? UpperRightLatitude { get; set; }

        /// <summary>
        /// UpperRight Longitude.
        /// </summary>
        public double? UpperRightLongitude { get; set; }
    }
}
