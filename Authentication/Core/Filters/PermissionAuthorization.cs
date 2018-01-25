﻿
using Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Filters
{
    //自定义授权策略
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {

    }
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            //if (true)
            //{
            //    context.Succeed(requirement);
            //    return Task.CompletedTask;
            //}
            // context.Fail();
            return Task.CompletedTask;
        }
    }
    // 使用过滤器授权
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(Module moduleName, Operation operationName) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(moduleName.ToString(), operationName.ToString()) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
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
                var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value.Split(",").Contains(_claim.Value));
                if (!hasClaim)
                {
                    // to denied page
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
