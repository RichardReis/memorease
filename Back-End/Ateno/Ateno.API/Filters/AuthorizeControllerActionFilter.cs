using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;

namespace Ateno.API.Filters
{
    public class AuthorizeControllerActionFilter : IActionFilter
    {

        public AuthorizeControllerActionFilter()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var c = context.HttpContext.Request;
            //List<EndPointMapRoles> response = _endpointMapRolesRepository
            //    .FindRolesByEndPointAndVerb(context.HttpContext.Request.Path.Value, context.HttpContext.Request.Method);

            //if (response is not null)
            //{
            //    if (context.HttpContext.User.Identity.IsAuthenticated)
            //    {
            //        bool flagAllowAccess = false;

            //        List<string> responseRoles = response.Select(x => x.RoleUser.Name).ToList();

            //        IEnumerable<Claim> claimRoles = context.HttpContext.User.FindAll(ClaimTypes.Role);

            //        foreach (Claim item in claimRoles)
            //        {
            //            if (responseRoles.Contains(item.Value))
            //                flagAllowAccess = true;
            //        }

            //        if (!flagAllowAccess)
            //        {
            //            ObjectResult result = new ObjectResult(new { erro = "Não autorizado." });
            //            result.StatusCode = StatusCodes.Status403Forbidden;
            //            context.Result = result;
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        ObjectResult result = new ObjectResult(new { erro = "Não autorizado." });
            //        result.StatusCode = StatusCodes.Status401Unauthorized;
            //        context.Result = result;
            //        return;
            //    }
            //}
        }
    }
}
