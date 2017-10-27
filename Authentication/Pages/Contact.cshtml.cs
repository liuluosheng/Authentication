using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Authentication.Pages
{
    [Authorize(Roles = "Admin")]
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "需要登录才能访问的：" + HttpContext.User.Identity.Name;
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            foreach (var c in identity.Claims)
            {
                Message = Message + c.Type + ":" + c.Value + ",";
            }
        }
    }
}
