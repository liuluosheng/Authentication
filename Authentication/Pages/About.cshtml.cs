using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Authentication.Permissions;

namespace Authentication.Pages
{
    [Module(Name = Modules.关于我们)]
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        [Permission(Name = Operations.新建)]
        public void OnGet()
        {
            Message = "Your application description page.";
        }
    }
}
