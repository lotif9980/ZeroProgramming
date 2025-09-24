using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ZPWEB.Controllers
{  
    public class AccountController : Controller
    {
        private const string DefaultUsername = "admin";
        private const string DefaultPassword = "1234";
        private const string UName = "support";
        private const string UPassword = "Test_123";

        //asfdsafd
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
       
        public IActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public IActionResult Login(string username, string password)
        //{
        //    if (username == DefaultUsername && password == DefaultPassword)
        //    {
        //        HttpContext.Session.SetString("IsLoggedIn", "true");
        //        HttpContext.Session.SetString("Username", username);

        //        return RedirectToAction("Index", "Home");
        //    }

        //    ViewBag.Error = "❌ Invalid username or password!";
        //    return View();
        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
          if((username == DefaultUsername && password == DefaultPassword)|| (username == UName && password == UPassword))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false, // 🔹 browser বন্ধ করলে cookie থাকবে না
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // optional timeout
                };

                await HttpContext.SignInAsync(
                    "MyCookieAuth",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
          }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "Account");
        }
    }
}
