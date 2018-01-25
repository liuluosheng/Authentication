
using Authentication.Enums;
using Authentication.Filters;
using Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Core.Filters;
namespace Authentication.Controllers
{
    [InternalAuthorize(Module.用户中心)]
    public class UserController : Controller
    {
        [Operation(Name = Operation.查询 | Operation.创建)]
        public IActionResult Index(string name = "1")
        {
            return View();
        }
    }
}
