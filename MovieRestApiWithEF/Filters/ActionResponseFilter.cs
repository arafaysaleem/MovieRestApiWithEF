using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities.Responses;
using System.Net;

namespace MovieRestApiWithEF.Filters
{
    public class ActionResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = new ApiResponse(
                    statusCode: (HttpStatusCode)objectResult.StatusCode!,
                    message: "Request completed successfully",
                    body: objectResult.Value!
                );
            }
            else if (context.Result is NoContentResult noContentResult)
            {
                var result = new ObjectResult(null)
                {
                    Value = new ApiResponse(
                        statusCode: HttpStatusCode.OK,
                        message: "Request completed successfully",
                        body: new { }
                    ),
                };
                context.Result = result;
            }
        }
    }
}
