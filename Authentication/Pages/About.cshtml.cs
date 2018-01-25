using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Authentication.Enums;
using Authentication.Filters;

namespace Authentication.Pages
{
    // [Permission(Modules.关于我们, Operations.Delete)]
    [ClaimRequirement(Module.关于我们, Operation.查询)]
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
