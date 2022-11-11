using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Core.Helpers;
using NavigatorAttractions.Service.Models.Keyword;
using NavigatorAttractions.Service.Models.ReferenceTypes;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Reference Controller.
    /// </summary>
    [Route("api/references")]
    //[EnableCors("AllowAll")]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceService _referenceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceController"/> class.
        /// </summary>
        /// <param name="referenceService"></param>
        public ReferenceController(IReferenceService referenceService)
        {
            this._referenceService = referenceService ?? throw new ArgumentNullException(nameof(referenceService));
        }

        /// <summary>
        ///  Get ReferenceType.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("referenceTypes/{id}")]
        [ProducesResponseType(typeof(ReferenceTypeModel), 200)]
        [Produces("application/json", Type = typeof(ReferenceTypeModel))]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var results = await _referenceService.GetReferenceType(id);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        /// <summary>
        ///  Get ReferenceTypes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("referenceTypes")]
        [ProducesResponseType(typeof(List<ReferenceTypeModel>), 200)]
        [Produces("application/json", Type = typeof(List<ReferenceTypeModel>))]
        public async Task<IActionResult> Get()
        {
            var results = await _referenceService.GetReferenceTypes();
            return Ok(results);
        }

        /// <summary>
        /// Gets List of References for Type.
        /// </summary>
        /// /// <param name="type">book,dataSource</param>
        /// <returns></returns>
        [HttpGet]
        [Route("referenceTypes/list/{type}")]
        [Produces("application/json", Type = typeof(ReferenceTypeModel))]
        [ProducesResponseType(typeof(ReferenceTypeModel), 200)]
        public async Task<IActionResult> GetReferenceTypeList(string type)
        {
            if (type == null)
            {
                return BadRequest();
            }

            // Validate Type Exists
            var list = await _referenceService.GetReferenceTypes();
            var exist = (from r in list where r.Key == type select r.Key).Any();
            if (!exist)
            {
                return NotFound("Type Not Found");
            }

            string referenceType = $"{type}Type".UppercaseFirst();

            var results = await _referenceService.GetReferenceTypeList(referenceType);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        /// <summary>
        ///  List of Catalogs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("catalogs")]
        [Produces("application/json", Type = typeof(List<KeyValuePair<string, string>>))]
        [ProducesResponseType(typeof(List<KeyValuePair<string, string>>), 200)]
        public async Task<IActionResult> GetCatalogs()
        {
            var results = await _referenceService.GetCatalogs();
            return Ok(results);
        }

        /// <summary>
        ///  List of Features.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("features")]
        [Produces("application/json", Type = typeof(List<KeyValuePair<string, string>>))]
        [ProducesResponseType(typeof(List<KeyValuePair<string, string>>), 200)]
        public async Task<IActionResult> GetFeatures()
        {
            var results = await _referenceService.GetFeatures();
            return Ok(results);
        }

        /// <summary>
        ///  List of Display Types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("displayTypes")]
        [ProducesResponseType(typeof(List<KeyValuePair<string, string>>), 200)]
        [Produces("application/json", Type = typeof(List<KeyValuePair<string, string>>))]
        public async Task<IActionResult> GetDisplayTypes()
        {
            var results = await _referenceService.GetDisplayTypes();
            return Ok(results);
        }

        /// <summary>
        ///  List of Object Types.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("objectTypes")]
        [ProducesResponseType(typeof(List<KeyValuePair<string, string>>), 200)]
        [Produces("application/json", Type = typeof(List<KeyValuePair<string, string>>))]
        public async Task<IActionResult> GetObjectTypes()
        {
            var results = await _referenceService.GetObjectTypes();
            return Ok(results);
        }

        /// <summary>
        ///  List of Keywords.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("keywords")]
        [Produces("application/json", Type = typeof(List<KeywordModel>))]
        [ProducesResponseType(typeof(List<KeyValuePair<string, string>>), 200)]
        public async Task<IActionResult> GetKeywords()
        {
            var results =  await _referenceService.GetKeywords();
            return Ok(results);
        }
    }
}
