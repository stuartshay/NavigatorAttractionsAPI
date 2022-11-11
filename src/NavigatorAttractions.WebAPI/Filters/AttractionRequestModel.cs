using NavigatorAttractions.Data.Entities.ReferenceTypes;
using Newtonsoft.Json;

namespace NavigatorAttractions.WebAPI.Filters
{
    /// <summary>
    ///  Attraction Search Request.
    /// </summary>
    public class AttractionRequestModel : RequestModel
    {
        /// <summary>
        /// Namespace Catalog.
        /// </summary>
        #warning Need to get this too work
        [JsonProperty("query.catalog")]
        public string? Catalog { get; set; }

        /// <summary>
        ///  Feature Type.
        /// </summary>
        public string? Feature { get; set; }

        /// <summary>
        ///  Object Type.
        /// </summary>
        public string? ObjectType { get; set; }

        /// <summary>
        /// Photo Size.
        /// </summary>
        public string? PhotoSize { get; set; }

        /// <summary>
        /// Text Search.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        ///  List/Grid Results.
        /// </summary>
        public string? Display { get; set; }

        /// <summary>
        ///  Fields Filter.
        /// </summary>
        public string? FieldsList { get; set; }

        /// <summary>
        /// Keyword Match All/Any (Default).
        /// </summary>
        public string? KeywordsMatch { get; set; }

        /// <summary>
        /// Keyword List Comma Separated.
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// Reference Type Filter.
        /// </summary>
        public ReferenceType? ReferenceType { get; set; }

        /// <summary>
        /// Map Location Filter.
        /// </summary>
        public LocationType LocationType { get; set; }
    }
}
