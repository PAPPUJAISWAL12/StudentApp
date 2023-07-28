using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApp.Models;
using System.Security.Claims;

namespace StudentApp.Controllers
{
    public class HomeController : Controller
    {
        private StudentBlogContext _context;
        public HomeController(StudentBlogContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return PartialView("_Index");
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserList userlist)
        {
            List<UserList>? us = _context.UserLists.ToList();
            if(us != null)
            {
                UserList? u = us.Where(x => x.UserName.ToUpper().Equals(userlist.UserName.ToUpper()) && x.UserPassword.Equals(userlist.UserPassword.ToString())).FirstOrDefault();
                if(u != null)
                {
                    var claims = new List<Claim> { 
                                new Claim(ClaimTypes.Name,u.UserId.ToString()),
                                new Claim("UserName",u.UserName.ToString()),
                                new Claim(ClaimTypes.Role,u.UserRoleType)                    
                    };
                   
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                   await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });
                    return RedirectToAction("dashbord","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Failled Please Check Your Username or Password");
                }
            }
            return PartialView("_Index", userlist);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Students");
        }


       
        public IActionResult dashbord()
        {
            if (User.IsInRole("Admin"))
            {
				return RedirectToAction("Index","UserLists");
			}
            else if(User.IsInRole("User"))
            {
                return RedirectToAction("Details","UserLists");
            }
            else
            {
                return RedirectToAction("Index","Students");
            }
        }

	}
}

