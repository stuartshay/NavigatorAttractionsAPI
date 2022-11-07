using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using NavigatorAttractions.Service.Models.Attractions;
using Swashbuckle.AspNetCore.Filters;

namespace NavigatorAttractions.WebAPI.Extentsions
{
    public class SwaggerExampleAttraction : IExamplesProvider<JsonPatchDocument<AttractionModel>>
    {
        public JsonPatchDocument<AttractionModel> GetExamples()
        {
            return new JsonPatchDocument<AttractionModel>
            {
                Operations = 
                { 
                    new Operation<AttractionModel> { op = "replace", path = "/title", value = "My Sample Title", }
                }
            };
        }
    }
}
