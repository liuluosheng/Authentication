using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Permissions
{
    public class PermissionClaim
    {
        public Modules M { get; set; }
        public Operations P { get; set; }
    }
}
