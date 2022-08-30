using Microsoft.AspNetCore.Mvc.Filters;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Extensions;

namespace MovieRestApiWithEF.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Get the body passed to the request
            var param = context.ActionArguments.SingleOrDefault();
            
            // Check if body is missing
            if (param.Value == null)
            {
                throw new BadRequestException("Request data sent from client is null");
            }

            // Check validation for the body
            if (!context.ModelState.IsValid)
            {
                throw new UnprocessibleEntityException(
                    "Invalid object sent from client",
                    details: context.ModelState.toJson() // toJson gets the model errors in a readable form
                );
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
