//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Data.Entitys;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;
//using Authentication.Models;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using Authentication.Enums;
//using Microsoft.AspNetCore.Authorization;
//using Core.IServices;

//namespace Authentication.Pages
//{
//    [AllowAnonymous]
//    public class LoginModel : PageModel
//    {
//        private IBaseService<User> _userService;
//        public LoginModel(IBaseService<User> userService)
//        {
//            _userService = userService;
//        }
//        [AllowAnonymous]
//        public void OnGet()
//        {
//        }

//        public async Task<IActionResult> OnPost(UserLogin user, string ReturnUrl = "\\")
//        {

//            var model = await _userService.Entities.Include("Roles.Role")
//                .FirstOrDefaultAsync(p => (p.Name == user.Name || p.Mobile == user.Name) && p.PassWord == user.PassWord);
//            if (model != null)
//            {
//                ClaimsIdentity claims = new ClaimsIdentity("Passport");
//                claims.AddClaim(new Claim(ClaimTypes.Name, user.Name, ClaimValueTypes.String));
//                foreach (var p in model.Roles)
//                {
//                    claims.AddClaim(new Claim(ClaimTypes.Role, p.Role.Name, ClaimValueTypes.String, ClaimsIdentity.DefaultIssuer)); //��ɫ��Ȩ
//                }
//                var permisson = model.Roles.Select(p => JsonConvert.DeserializeObject<Dictionary<Module, Operation>>(p.Role.Permissions)).SelectMany(p => p).Distinct();
//                foreach (var p in permisson)
//                {
//                    claims.AddClaim(new Claim(p.Key.ToString(), p.Value.ToString(), ClaimValueTypes.String)); //������Ȩ
//                }
//                await HttpContext.SignInAsync(Startup.AuthenticationSchemeName, new ClaimsPrincipal(claims), new AuthenticationProperties
//                {
//                    IsPersistent = user.IsPersistent,
//                    AllowRefresh = true
//                });
//                return Redirect(ReturnUrl);
//            }
//            return Redirect("/");
//        }
//    }
//}