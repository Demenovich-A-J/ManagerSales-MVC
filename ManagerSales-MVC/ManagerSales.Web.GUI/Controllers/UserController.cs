using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagerSales.Web.GUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ManagerSales.Web.GUI.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View("Users");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var userName = userManager.Users.FirstOrDefault(x => x.Id == id);
            if (userName != null)
            {
                ViewBag.UserName = userName.UserName;
            }

            ViewBag.Roles = roleManager.Roles.ToList().Select(c => new SelectListItem
            {
                Value = c.Id,
                Text = c.Name
            });

            return View(new Role() {UserId = id});
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddRole(Role role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var user = userManager.Users.FirstOrDefault(x => x.Id == role.UserId);
            
            if (user != null && user.Roles.FirstOrDefault(x => x.RoleId == role.Id) == null) user.Roles.Add(new IdentityUserRole { RoleId = role.Id, UserId = role.UserId });
            userManager.Update(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult UsersGrid()
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ViewBag.Manager = userManager;
            return PartialView("UserGrid", userManager.Users.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddUser()
        {
            return View("AddUser");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveUser(string id)
        {
            var userManager = HttpContext.GetOwinContext()
                .GetUserManager<ApplicationUserManager>();
            userManager.Delete(userManager.Users.FirstOrDefault(x => x.Id == id));

            return RedirectToAction("Index", "User");
        }
    }
}