using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Service.Models.Attractions;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.WebAPI.Constants;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Attraction Controller.
    /// </summary>
    [Route(RouteConstants.AttractionRoute)]
    //[EnableCors("AllowAll")]
    public class AttractionCommandController : ControllerBase
    {
        private readonly IAttractionService _attractionService;

        private readonly IPhotoService _photoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttractionCommandController"/> class.
        /// </summary>
        /// <param name="attractionService"></param>
        /// <param name="photoService"></param>
        public AttractionCommandController(IAttractionService attractionService, IPhotoService photoService)
        {
            _attractionService = attractionService ?? throw new ArgumentNullException(nameof(attractionService));
            _photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
        }

        /// <summary>
        ///  Update Attraction Primary Photo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="photoId"></param>
        /// <returns></returns>
        [Route("{id}/{photoId}")]
        [HttpPut]
        [ProducesResponseType(typeof(AttractionModel), 200)]
        [Produces("application/json", Type = typeof(AttractionModel))]
        public async Task<IActionResult> Put(string id, string photoId)
        {
            var attraction = await _attractionService.GetAttraction(id);
            if (attraction == null)
                return BadRequest();

            var photo = await _photoService.GetPhoto(photoId);
            if (photo == null)
                return BadRequest();

            //var photoResult = PhotoHelpers.BuildPhotoModel(photo, "t");
            //var photoModel = new AttractionPhotoModel
            //{
            //    Id = photo.Id,
            //    PhotoId = photo.PhotoId,
            //    Title = photo.Title,
            //    Height = photoResult.Height,
            //    Width = photoResult.Width,
            //    Url = photoResult.Url,
            //};

            //var patchAttractionPhoto = new JsonPatchDocument<AttractionModel>();
            //patchAttractionPhoto.Replace<AttractionPhotoModel>(o => o.Photo, photoModel);
            //patchAttractionPhoto.ApplyTo(attraction);

            //var result = await _attractionService.UpdateAttraction(attraction);
            //if (result.Status == ResultConstants.ExceptionStatus)
            //{
            //    return UnprocessableEntity(result.Status);
            //}

            //return Ok(result.Entity);
            return Ok();
        }

        /// <summary>
        ///  Patch Attraction.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="value">RFC 6902 Patch Request.</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpPatch]
        [ProducesResponseType(typeof(AttractionModel), 200)]
        [Produces("application/json", Type = typeof(AttractionModel))]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<AttractionModel> value, string id = "533cddaf5c9596ef08143d56")
        {
            if (value == null)
                return BadRequest();

            var attraction = await _attractionService.GetAttraction(id);
            if (attraction == null)
                return NotFound();

            value.ApplyTo(attraction);
            var result = await _attractionService.UpdateAttraction(attraction);

            return Ok(result.Entity);
        }
    }
}