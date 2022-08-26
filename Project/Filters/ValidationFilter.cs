using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities.Responses;
using System.Net;
using System.Security.Principal;
using MovieRestApiWithEF.Exceptions;
using MovieRestApiWithEF.Extensions;
using Entities.Models;

namespace MovieRestApiWithEF.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault();
            if (param.Value == null)
            {
                throw new BadRequestException("Request data sent from client is null");
            }

            if (!context.ModelState.IsValid)
            {
                throw new UnprocessibleEntityException(
                    "Invalid object sent from client",
                    details: context.ModelState.toJson());
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
