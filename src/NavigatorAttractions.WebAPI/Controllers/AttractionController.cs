using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Constants;
using NavigatorAttractions.WebAPI.Enums;
using NavigatorAttractions.WebAPI.Filters;
using NavigatorAttractions.Data.Filters.GeoRequest;


namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Attraction Controller.
    /// </summary>
    [Route(RouteConstants.AttractionRoute)]
    public class AttractionController : ControllerBase
    {
        private readonly IAttractionService _attractionService;

        private readonly ILogger<AttractionController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttractionController"/> class.
        /// </summary>
        /// <param name="attractionService"></param>
        /// <param name="logger"></param>
        public AttractionController(IAttractionService attractionService, ILogger<AttractionController> logger)
        {
            _attractionService = attractionService ?? throw new ArgumentNullException(nameof(attractionService));
            _logger = logger;
        }

        /// <summary>
        ///  Get Attraction.
        /// </summary>
        /// <param name="id">Object Id.</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(AttractionModel), 200)]
        [Produces("application/json", Type = typeof(AttractionModel))]
        public async Task<IActionResult> Get(string id = "533cddaf5c9596ef08143d56")
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var attraction = await _attractionService.GetAttraction(id);
            if (attraction == null)
            {
                return NotFound();
            }

            if (attraction.Photo != null)
            {
                attraction.Photo.Width = (int)Math.Ceiling(attraction.Photo.Width * 2.4);
                attraction.Photo.Height = (int)Math.Ceiling(attraction.Photo.Height * 2.4);
                attraction.Photo.Url = !string.IsNullOrEmpty(attraction?.Photo?.Url) ? attraction.Photo.Url.Replace("t.jpg", "m.jpg") : null;
            }

            _logger.LogInformation("{@attraction}", attraction);
            return Ok(attraction);
        }

        /// <summary>
        ///  Paging and Filtered Search.
        /// </summary>
        /// <param name="query">Query String Params.</param>
        /// <param name="limit">Records per Page.</param>
        /// <param name="page">Page Number.</param>
        /// <returns></returns>
        [Route("{limit:int}/{page:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultModel<AttractionModel>), 200)]
        [Produces("application/json", Type = typeof(PagedResultModel<AttractionModel>))]
        public async Task<IActionResult> GetList([FromQuery] AttractionRequestModel query, int limit = 10, int page = 1)
        {
            // Set Default Photo Size
            query.PhotoSize = !string.IsNullOrEmpty(query.PhotoSize) ? query.PhotoSize : "t";

            // Display Filter
            if (query?.Display != null)
            {
                query = FilterDisplay(query);
            }

            // Location Filter
            GeoWithin? location = query?.LocationType != null ? FilterLocation(query) : null;

            var attractionRequest = new AttractionRequest
            {
                PageSize = limit,
                Page = page,
                SortColumn = !string.IsNullOrEmpty(query?.Sort) ? query.Sort : "title",
                SortOrder = !string.IsNullOrEmpty(query?.Order) ? query.Order : "asc",
                Catalog = query?.Catalog,
                ObjectType = query?.ObjectType,
                Feature = query?.Feature,
                PhotoSize = query?.PhotoSize,
                KeywordMatch = query?.KeywordsMatch?.Trim().ToLower() == "all" ? "all" : null,
                KeywordList = query?.Keywords?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                FieldsList = query?.FieldsList?.RemoveWhitespace().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                //ReferenceId = query.ReferenceType?.TypeId != null ? query.ReferenceType.TypeId : null,
                Search = query?.Search,
                Location = location,
            };

            var results = await _attractionService.GetAttractions(attractionRequest);
            return Ok(results);
        }

        /// <summary>
        ///  Display Grid/List Column Filtering and Object Properties.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private AttractionRequestModel FilterDisplay(AttractionRequestModel query)
        {
            if (query.Display != null)
            {
                if (string.Equals(query.Display, OutputDisplay.GRID.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    query.PhotoSize = "m";
                    query.FieldsList = "id,title,catalog,featureKey,hasPhotos,photo";
                }
                else if (string.Equals(query.Display, OutputDisplay.LIST.ToString(),
                             StringComparison.OrdinalIgnoreCase))
                {
                    query.PhotoSize = "t";
                    query.FieldsList = "id,title,catalog,featureKey,hasPhotos,photo,loc,machineTags,geoCoordinate";
                }
            }

            return query;
        }

        private GeoWithin? FilterLocation(AttractionRequestModel query)
        {
            if (query?.LocationType != null)
            {
                // Radius
                if (string.Equals(query.LocationType.TypeId, "radius", StringComparison.OrdinalIgnoreCase) &&
                    query.LocationType.Latitude.HasValue && query.LocationType.Longitude.HasValue)
                {
                    // Radians=(Miles/3959) Radians=(Km/6371)
                    var radians = query.LocationType.Radius / 6371 ?? .1 / 6371;
                    return new GeoWithin
                    {
                        CenterSphere = new CenterSphere
                        {
                            Center = new Point(query.LocationType.Latitude.Value, query.LocationType.Longitude.Value),
                            Radius = radians,
                        },
                    };
                }

                // Bounding Box (TODO: Add Validation Rules)
                if (string.Equals(query.LocationType.TypeId, "box", StringComparison.OrdinalIgnoreCase))
                {
                    if (query.LocationType?.BoundingBox?.LowerLeftLatitude != null
                        && query.LocationType?.BoundingBox?.LowerLeftLongitude != null
                        && query.LocationType?.BoundingBox?.UpperRightLatitude != null
                        && query.LocationType?.BoundingBox?.UpperRightLongitude != null)
                    {
                        var lowerLeftLatitude = query.LocationType.BoundingBox.LowerLeftLatitude.Value;
                        var lowerLeftLongitude = query.LocationType.BoundingBox.LowerLeftLongitude.Value;
                        var upperRightLatitude = query.LocationType.BoundingBox.UpperRightLatitude.Value;
                        var upperRightLongitude = query.LocationType.BoundingBox.UpperRightLongitude.Value;

                        return new GeoWithin
                        {
                            Box = new Box
                            {
                                LowerLeft = new Point(lowerLeftLatitude, lowerLeftLongitude),
                                UpperRight = new Point(upperRightLatitude, upperRightLongitude),
                            },
                        };
                    }
                }
            }

            return null;
        }
    }
}