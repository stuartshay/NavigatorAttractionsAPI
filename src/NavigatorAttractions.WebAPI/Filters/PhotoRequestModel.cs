using Newtonsoft.Json;

namespace NavigatorAttractions.WebAPI.Filters
{
    /// <summary>
    /// Photo Request Model.
    /// </summary>
    public class PhotoRequestModel
    {
        /// <summary>
        ///  Flickr Photo Size (s,q,t,m,n).
        /// </summary>
        [JsonProperty("photoSize")]
        public string PhotoSize { get; set; }
    }
}
