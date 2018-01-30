
using Authentication.Enums;
using Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Core.Filters;
using Microsoft.AspNetCore.Authorization;
using Authentication.Core.Attributes;

namespace Authentication.Controllers
{

    public class UserController : Controller
    {
        [OperationAuthorize(Module = Module.用户中心, Operation = Operation.删除)]
        public IActionResult Index()
        {
            return View();
        }
        [OperationAuthorize(Module = Module.产品中心, Operation = Operation.创建)]
        public IActionResult Post()
        {
            return View();
        }
    }
}
