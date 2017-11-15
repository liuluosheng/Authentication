using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.Entitys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Authentication.Models;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Authentication.Permissions;

namespace Authentication.Pages
{
    public class LoginModel : PageModel
    {
        private IBaseRepository<User> _userService;
        public LoginModel(IBaseRepository<User> userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(UserLogin user, string ReturnUrl = "\\")
        {
            var model = await _userService.Entities.Include("Roles.Role")
                .FirstOrDefaultAsync(p => (p.Name == user.Name || p.Mobile == user.Name) && p.PassWord == user.PassWord);
            if (model != null)
            {
                ClaimsIdentity claims = new ClaimsIdentity("Passport");
                claims.AddClaim(new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String));
                foreach (var p in model.Roles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, p.Role.Name, ClaimValueTypes.String, ClaimsIdentity.DefaultIssuer)); //角色授权
                }
                var permisson = model.Roles.Select(p => JsonConvert.DeserializeObject<Dictionary<Modules, Operations[]>>(p.Role.Permissions)).SelectMany(p => p).Distinct();

                foreach (var p in permisson)
                {
                    claims.AddClaim(new Claim(p.Key.ToString(), string.Join(",", p.Value), ClaimValueTypes.String, ClaimsIdentity.DefaultIssuer)); //操作授权
                }
                await HttpContext.SignInAsync(Startup.AuthenticationSchemeName, new ClaimsPrincipal(claims), new AuthenticationProperties
                {
                    IsPersistent = user.IsPersistent,
                    AllowRefresh = true
                });
                return Redirect(ReturnUrl);
            }
            return Redirect("/");
        }
    }
}