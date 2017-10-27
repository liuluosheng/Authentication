using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Permissions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Module : Attribute
    {
        public ModuleName Name { get; set; }
    }
}
