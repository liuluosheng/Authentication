using Authentication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]

    public class OperationAttribute : Attribute
    {
        public Operation Name { get; set; }
    }
}
