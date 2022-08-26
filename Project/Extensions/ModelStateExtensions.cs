using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace MovieRestApiWithEF.Extensions
{
    public static class ModelStateExtensions
    {
        public static string toJson(this ModelStateDictionary modelState)
        {
            // Get key(s) and error message(s) from the ModelState
            var serializableModelState = new SerializableError(modelState);

            // Convert to a string
            return JsonConvert.SerializeObject(serializableModelState);
        }
    }
}
