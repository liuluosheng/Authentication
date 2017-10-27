
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Authentication.Permissions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public ModuleName ModuleName { get; private set; }
        public PermissionName PermissionName { get; private set; }

        public PermissionRequirement(ModuleName moduleName, PermissionName permissionName)
        {
            ModuleName = moduleName;
            PermissionName = permissionName;
        }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            foreach (var claim in context.User.Claims.Where(p => p.Type == "Permission"))
            {
                PermissionItem permission = JsonConvert.DeserializeObject<PermissionItem>(claim.Value);
                if (permission.M == requirement.ModuleName && permission.P == requirement.PermissionName)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
