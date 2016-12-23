using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WhereAreMyMoney_2._0.Models;

namespace WhereAreMyMoney_2._0.Controllers
{
    public class TestGetUserInfoController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public async Task<ActionResult> Index()
        {
             var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            return View(user);
        }

    }
}