using CrmAssistant.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CrmAssistant.Models.Validation;
using CrmAssistant.Models.Enums;

namespace CrmAssistant.Controllers
{
    public class AuthenticationController : Controller
    {

        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly PubContext _pubContext;

        public AuthenticationController(PubContext pubContext)
        {
            _pubContext = pubContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticationRequest authenticationRequest)
        {
            if (ModelState.IsValid)
            {

                var user = await _pubContext.Users
                    .Where(x => x.Email.Equals(authenticationRequest.Email))
                    .Where( x => x.Password.Equals(authenticationRequest.Password))
                    .SingleOrDefaultAsync();

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    //new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Redirect("/Home/Index");
            }

            return View();
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");
        }
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


    }
}
