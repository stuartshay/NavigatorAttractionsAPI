using Microsoft.AspNetCore.Mvc;
using NavigatorAttractions.Service.Models.MachineKeys;
using NavigatorAttractions.Service.Services.Interface;

namespace NavigatorAttractions.WebAPI.Controllers
{
    /// <summary>
    /// Machine Key Controller.
    /// </summary>
    [Route("api/[controller]")]
    //[EnableCors("AllowAll")]
    public class MachineKeyController : Controller
    {
        private readonly IAttractionService _attractionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineKeyController"/> class.
        /// </summary>
        /// <param name="attractionService"></param>
        public MachineKeyController(IAttractionService attractionService)
        {
            this._attractionService = attractionService ?? throw new ArgumentNullException(nameof(attractionService));
        }

        /// <summary>
        /// Get Machine Keys.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        [Produces("application/json", Type = typeof(List<string>))]
        public async Task<IActionResult> Get()
        {
            var results = await _attractionService.GetMachineKeys();
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-InlineCount");
            HttpContext.Response.Headers.Add("X-InlineCount", results.Count.ToString());

            return Ok(results);
        }

        /// <summary>
        /// Get Predicates.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("predicates")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [Produces("application/json", Type = typeof(List<string>))]
        public async Task<IActionResult> GetPredicates()
        {
            var results = await _attractionService.GetPredicates();
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-InlineCount");
            HttpContext.Response.Headers.Add("X-InlineCount", results.Count.ToString());

            return Ok(results);
        }

        /// <summary>
        /// Validate Machine Key.
        /// </summary>
        /// <param name="k">Machine Key.</param>
        /// <returns>Key Validation List.</returns>
        [HttpGet]
        [Route("Validation")]
        [ProducesResponseType(typeof(List<MachineKeyResultModel>), 200)]
        [Produces("application/json", Type = typeof(List<MachineKeyResultModel>))]
        public async Task<IActionResult> GetKeysValidation([FromQuery] string[] k)
        {
            if (k == null)
            {
                return BadRequest();
            }

            var results = new List<MachineKeyResultModel>();
            foreach (var key in k)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    var exists = await _attractionService.ValidateMachineKey(key.Trim());
                    results.Add(
                        new MachineKeyResultModel
                        {
                            MachineKey = key.ToLower(),
                            Result = exists,
                        });
                }
            }

            return Ok(results);
        }
    }
}
