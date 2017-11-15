using Authentication.Permissions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    public class UserController : Controller
    {
        [ClaimRequirement(Modules.关于我们, Operations.Read)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
