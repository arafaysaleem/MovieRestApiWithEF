using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace MovieRestApiWithEF.Extensions
{
    public static class ModelStateExtensions
    {
        public static Dictionary<string, string[]> toJson(this ModelStateDictionary modelState)
        {
            // Get key(s) and error message(s) from the ModelState
            var errors = modelState
                .Where(x => (x.Value?.Errors.Count ?? 0)  > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            // return errors list
            return errors;
        }
    }
}
