using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Data;
using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Signup()
        {
            if (User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Feed");
            }

            return View();
        }
        public IActionResult Login()
        {
            if (User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Feed");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserId, string Password)
        {
            var User = _context.Users.Where(User => User.UserId == UserId && User.Password == Password).FirstOrDefault();
            if (User != null)
            {
                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, UserId)
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return Redirect("/Feed");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup([Bind("UserId, Name, Password")] User user)
        {
            if (ModelState.IsValid && _context.Users.ToList().Count() < 100)
            {
                user.UserType = UserType.User;
                _context.Add(user);
                await _context.SaveChangesAsync();

                var User = _context.Users.Where(User => User.UserId == user.UserId && User.Password == user.Password).FirstOrDefault();

                if (User != null)
                {
                    //Create the identity for the user  
                    var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserId)
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Redirect("/Feed");
                }
            }

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
