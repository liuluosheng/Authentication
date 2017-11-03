using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Authentication.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace Authentication.Pages
{
    // [Permission(Modules.关于我们, Operations.Delete)]
    [ClaimRequirement(Modules.关于我们, Operations.Read)]
    public class AboutModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
