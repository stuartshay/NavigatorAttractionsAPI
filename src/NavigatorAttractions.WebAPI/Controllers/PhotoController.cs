using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Core.Models;
using NavigatorAttractions.Data.Enums;
using NavigatorAttractions.Data.Filters;
using NavigatorAttractions.Service.Builders;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Constants;
using NavigatorAttractions.WebAPI.Filters;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Photo Controller.
    /// </summary>
    [Route(RouteConstants.PhotoRoute)]
    //[EnableCors("AllowAll")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IAttractionService _attractionService;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoController"/> class.
        /// </summary>
        /// <param name="photoService"></param>
        /// <param name="attractionService"></param>
        /// <param name="logger"></param>
        public PhotoController(IPhotoService photoService, IAttractionService attractionService, ILogger<PhotoController> logger)
        {
            this._photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
            this._attractionService = attractionService ?? throw new ArgumentNullException(nameof(attractionService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get Photo.
        /// </summary>
        /// <param name="id">Guid 5347677e85638e15bda6413d</param>
        /// <param name="photoSize">Photo Size (s,t,n).</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(PhotoModel), 200)]
        [Produces("application/json", Type = typeof(PhotoModel))]
        public async Task<IActionResult> Get(string id = "5347677e85638e15bda6413d", [FromQuery] string photoSize = "n")
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = await _photoService.GetPhoto(id);
            if (result == null)
                return NotFound();

            var photo = PhotoHelpers.BuildPhotoModel(result, photoSize);
            return Ok(photo);
        }

        ///// <summary>
        /////  Paging List of Photos.
        ///// </summary>
        ///// <param name="id">Attraction Id.</param>
        ///// <param name="limit">Records per Page.</param>
        ///// <param name="page">Page Number.</param>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("{id}/{limit:int}/{page:int}")]
        //[ProducesResponseType(typeof(PagedResultModel<PhotoGalleryModel>), 200)]
        //[Produces("application/json", Type = typeof(PagedResultModel<PhotoGalleryModel>))]
        //public async Task<IActionResult> Get(string id, int limit, int page, [FromQuery] PhotoRequestModel query)
        //{
        //    var attraction = await _attractionService.GetAttraction(id);
        //    if (attraction == null)
        //        return NotFound();

        //    var tags = attraction.MachineTags?.Select(x => x.Tag);
        //    if (tags == null)
        //        return NotFound();

        //    var photoRequest = new PhotoRequest
        //    {
        //        PageSize = limit,
        //        Page = page,
        //        SortColumn = string.Empty,
        //        Tags = tags.ToArray(),
        //        PhotoSize = query?.PhotoSize ?? "m",
        //    };

        //    var results = await _photoService.GetPhotos(photoRequest);
        //    return Ok(results);
        //}

        /// <summary>
        /// Get Photo Status.
        /// </summary>
        /// <param name="photoId">Flickr PhotoId.</param>
        /// <param name="lastUpdated">Last Updated DateTime.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("status/{photoId:long}")]
        [ProducesResponseType(typeof(PhotoStatus), 200)]
        [Produces("application/json", Type = typeof(PhotoStatus))]
        public async Task<IActionResult> GetPhotoStatus(long photoId, [FromQuery] DateTime? lastUpdated)
        {
            var status = await _photoService.ValidatePhotoStatus(photoId, lastUpdated);
            return Ok(status.ToString());
        }
    }
}
