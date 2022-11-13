using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NavigatorAttractions.Data.Results;
using NavigatorAttractions.Service.Models.Photos;
using NavigatorAttractions.Service.Results;
using NavigatorAttractions.Service.Services.Interface;
using NavigatorAttractions.Service.ValidationRules;
using NavigatorAttractions.WebAPI.Constants;
using Newtonsoft.Json;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Photo Command Controller.
    /// </summary>
    [Route(RouteConstants.PhotoRoute)]
    //[EnableCors("AllowAll")]
    public class PhotoCommandController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoCommandController"/> class.
        /// </summary>
        /// <param name="photoService"></param>
        /// <param name="logger"></param>
        public PhotoCommandController(IPhotoService photoService, ILogger<PhotoController> logger)
        {
            this._photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Save Photo.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PhotoModel), 201)]
        [Produces("application/json", Type = typeof(PhotoModel))]
        [Consumes("application/json")]
        public async Task<IActionResult> Post([FromBody, Required] PhotoModel model)
        {
            if (model == null)
                return BadRequest();

            var validator = new PhotoRule();
            var validationResults = validator.Validate(model);

            if (!validationResults.IsValid)
            {
                var results = validationResults.Errors.Select(v => new { v.PropertyName, v.ErrorMessage }).ToJson();
                _logger.LogError(JsonConvert.SerializeObject(results));

                var validationresult = new EntityResultModel<PhotoModel>
                {
                    Entity = model,
                    Status = ResultConstants.VaildationError,
                    StatusCode = StatusCodes.Status406NotAcceptable,
                    ValidationErrors = results,
                };

                return StatusCode(StatusCodes.Status406NotAcceptable, validationresult);
            }

            var result = await _photoService.SaveAsync(model);
            return Ok(result);
        }
    }
}
