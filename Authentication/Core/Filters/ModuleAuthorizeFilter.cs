using Authentication.Attributes;
using Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Authentication.Core.Filters
{
    public class ModuleAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private Module Module { get; set; }
        private Operation Operation { get; set; }
        public ModuleAuthorizeFilter(Module module)
        {
            Module = module;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {

                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    // to login page
                    context.Result = new ChallengeResult();
                }
                else
                {
                    var op = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(OperationAttribute), true);
                    if (op.Any())
                    {
                        Operation = (op.First() as OperationAttribute).Name;
                        var authorizationService = context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService)) as IAuthorizationService;
                        var isAuthorized = await authorizationService.AuthorizeAsync(context.HttpContext.User, new ModuleOperation { Module = Module, Operation = Operation }, "OperationPolicy");
                        if (!isAuthorized.Succeeded)
                        {
                            // to denied page
                            context.Result = new ForbidResult();
                        }
                    }
                }
            }
        }
    }
}
