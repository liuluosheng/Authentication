using Authentication.Attributes;
using Authentication.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InternalAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private Module Module { get; set; }
        public InternalAuthorizeAttribute(Module module)
        {
            Module = module;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
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
                    var operation = (op.First() as OperationAttribute).Name;
                    bool vaild = false;
                    foreach (var claim in context.HttpContext.User.Claims)
                    {
                        Operation _op;
                        Module _module;
                        if (Enum.TryParse(claim.Value, out _op) &&
                            Enum.TryParse(claim.Type, out _module) &&
                            ((_op & operation) != 0) &&
                            _module == Module)
                        {
                            vaild = true;
                            break;
                        }
                    }
                    if (!vaild)
                    {
                        // to denied page
                        context.Result = new ForbidResult();
                    }
                }

            }
        }
    }
}
