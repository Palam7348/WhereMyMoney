using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WhereAreMyMoney_2._0.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace WhereAreMyMoney_2._0.Controllers
{
    public class AccountController : Controller
    {

        private ApplicationContext db = new ApplicationContext();
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, Year = model.Year, Name = model.Name };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var newUser = await UserManager.FindAsync(model.Email, model.Password);
                    if (newUser != null)
                    {
                        db.Categories.Add(new Category { Name = "Продукты", Description = "Покупка продуктов на рынке и в супермаркетах", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Транспорт", Description = "Траты на общественный транспорт, переезды, билеты на поезда", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Зарплата", Description = "Зарплата с постоянного места работы", Type = "Доход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Квартира", Description = "Оплата за квартиру и коммунальные услуги", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Связь", Description = "Телефон и интернет", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Отдых", Description = "Поездки на отдых, развлечения", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Проценты", Description = "Доход от депозитов", Type = "Доход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Долги", Description = "Выплата долгов", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Кредиты", Description = "Выплаты по кредитам", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Автомобиль", Description = "Починка, ТО, бензин", Type = "Расход", UserId = newUser.Id });
                        db.Categories.Add(new Category { Name = "Образование", Description = "Курсы, университет, конференции", Type = "Расход", UserId = newUser.Id });
                        db.Accounts.Add(new Account { Name = "Кошелек", Amount = 0, UserId = newUser.Id });
                        db.Accounts.Add(new Account { Name = "Банковская карта №1", Amount = 0, UserId = newUser.Id });
                        db.Accounts.Add(new Account { Name = "Банковская карта №2", Amount = 0, UserId = newUser.Id });
                        db.SaveChanges();
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Edit()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel model = new EditModel { Year = user.Year };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.Year = model.Year;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
    }
}