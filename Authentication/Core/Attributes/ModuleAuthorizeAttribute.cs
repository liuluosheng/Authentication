using Authentication.Core.Filters;
using Authentication.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ModuleAuthorizeAttribute : TypeFilterAttribute
    {
        public ModuleAuthorizeAttribute(Module module) : base(typeof(ModuleAuthorizeFilter))
        {
            Arguments = new object[] { module };
        }
    }
}
