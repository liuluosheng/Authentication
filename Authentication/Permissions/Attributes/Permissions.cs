using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Permissions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class Permission : Attribute
    {
        public PermissionName Name { get; set; }
    }
}
