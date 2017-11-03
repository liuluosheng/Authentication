using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Permissions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]

    public class Operation : Attribute
    {
        public Operations Name { get; set; }
    }
}
