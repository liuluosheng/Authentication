using Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Core.Attributes
{

    /// <summary>
    /// eg:   [OperationAuthorize(Module = Module.产品中心, Operation = Operation.创建)]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class OperationAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {

        public Module Module { get; set; }
        public Operation Operation { get; set; }
        public IAuthorizationService AuthorizationService { get; set; }
        public OperationAuthorizeAttribute() : base()
        {

        }



        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                if (context.HttpContext.User.Identity.IsAuthenticated)
                {
                    AuthorizationService = context.HttpContext.RequestServices.GetService(typeof(IAuthorizationService)) as IAuthorizationService;
                    var isAuthorized = await AuthorizationService.AuthorizeAsync(context.HttpContext.User, new ModuleOperation { Module = Module, Operation = Operation }, "OperationPolicy");
                    if (!isAuthorized.Succeeded)
                    {
                        context.Result = new ForbidResult();
                    }
                }
                else
                {
                    context.Result = new ChallengeResult();
                }
            }
        }
    }
}
