using Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Core.Authorization
{
    //自定义授权策略
    public class OperationAuthorizationRequirement : IAuthorizationRequirement
    {
        //  public ModuleOperation ModuleOperation { get; set; }
    }
    public class OperationAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ModuleOperation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ModuleOperation resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }
            foreach (var claim in context.User.Claims)
            {
                Operation _op;
                Module _module;
                if (Enum.TryParse(claim.Value, out _op) &&
                    Enum.TryParse(claim.Type, out _module) &&
                    ((_op & resource.Operation) != 0) &&
                    _module == resource.Module)
                {
                    context.Succeed(requirement);
                    break;
                }
            }
            return Task.CompletedTask;
        }
    }
}
