using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Service.Models.PhotoCollections;
using NavigatorAttractions.Service.Results;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    ///     PhotoCollection Controller.
    /// </summary>
    [Route("api/photocollection")]
    public class PhotoCollectionController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PhotoCollectionController" /> class.
        /// </summary>
        /// <param name="photoService"></param>
        public PhotoCollectionController(IPhotoService photoService)
        {
            _photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
        }

        /// <summary>
        ///     PhotoCollection Status List.
        /// </summary>
        /// <param name="request">Request Model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("status")]
        //[SwaggerRequestExample(typeof(PhotoCollectionRequest), typeof(PhotoCollectionSearchModel))]
        [ProducesResponseType(typeof(PhotoCollectionStatusResult), 200)]
        [Produces("application/json", Type = typeof(PhotoCollectionStatusResult))]
        public async Task<IActionResult> GetPhotoCollectionStatus([FromBody][Required] List<PhotoCollectionRequest> request)
        {
            var results = new List<PhotoStatusResult>();

            foreach (var photo in request)
            {
                var status = await _photoService.ValidatePhotoStatus(long.Parse(photo.PhotoId),
                    DateTime.Parse(photo.LastUpdatedDate));
                results.Add(new PhotoStatusResult { PhotoId = photo.PhotoId, Status = status.ToString() });
            }

            return Ok(results);
        }
    }
}
