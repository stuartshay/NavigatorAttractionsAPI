namespace NavigatorAttractions.WebAPI.Filters
{
    /// <summary>
    ///
    /// </summary>
    public abstract class RequestModel
    {
        /// <summary>
        /// Sort Column.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        ///  Sort Order Direction.
        /// </summary>
        public string Order { get; set; }
    }
}
