using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Attributes;
using Authentication.Core.Attributes;
using Authentication.Core.Filters;
using Authentication.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [ModuleAuthorize(Module.用户中心)]
    public class DefaultController : Controller
    {
      
        [Operation(Name = Operation.创建)]
        public IActionResult Index()
        {
            return View();
        }
    }
}