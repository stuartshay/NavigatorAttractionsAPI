using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Constants;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Photo Attraction Controller.
    /// </summary>
    [Route("api/photo/attraction")]
    public class PhotoAttractionController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IAttractionService _attractionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoAttractionController"/> class.
        /// </summary>
        /// <param name="attractionService"></param>
        /// <param name="photoService"></param>
        public PhotoAttractionController(IAttractionService attractionService, IPhotoService photoService)
        {
            this._attractionService = attractionService ?? throw new ArgumentNullException(nameof(attractionService));
            this._photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
        }

        /// <summary>
        ///  Get Attractions for Photo.
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [Route("{photoId}")]
        [HttpGet]
        [ProducesResponseType(typeof(AttractionModel), 200)]
        [Produces("application/json", Type = typeof(AttractionModel))]
        public async Task<IActionResult> Get(long photoId = 5501115902)
        {
            if (photoId == 0)
            {
                return BadRequest();
            }

            var machineTags = await _photoService.GetPhotoMachineTags(photoId);
            if (machineTags.Count == 0)
                return NotFound(StatusMessageConstants.NotFoundMachineTag);

            var values = machineTags.Select(x => x.ToLower()).ToArray();
            var tags = new List<string>();
            foreach (var tag in values)
            {
                var exists = await _attractionService.ValidateMachineKey(tag.Trim());
                if (exists)
                    tags.Add(tag);
            }

            if (tags.Count == 0)
                return NotFound(StatusMessageConstants.NotFoundMachineTag);

            var results = await _attractionService.GetAttractions(tags.ToArray());
            if (results == null)
                return NotFound(StatusMessageConstants.NotFoundAttraction);

            return Ok(results);
        }
    }
}
