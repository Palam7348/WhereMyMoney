using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhereAreMyMoney_2._0.Models;
using Microsoft.AspNet.Identity;

namespace WhereAreMyMoney_2._0.Controllers
{
    public class OperationsController : Controller
    {
        private const string cost = "Расход";
        private ApplicationContext db = new ApplicationContext();

        // GET: Operations
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(currentUserId))
            {
                var allOperations = db.Operations.Include(p => p.Category).
                Include(c => c.Account).ToList();

                var ourOperations = allOperations.Where(e => e.Account.UserId.Equals(currentUserId)).
                    Where(e => e.Category.UserId.Equals(currentUserId)).ToList();
                ourOperations.Reverse();

                var categories = db.Categories.Where(e => e.UserId.Equals(currentUserId));
                var accounts = db.Accounts.Where(e => e.UserId.Equals(currentUserId));
                ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
                ViewBag.AccountId = new SelectList(accounts, "Id", "Name");
                #region DateAndTypeList
                List<SelectListItem> DateItems = new List<SelectListItem>();

                DateItems.Add(new SelectListItem { Text = "Показать за последние 3 дня", Value = "3" });

                DateItems.Add(new SelectListItem { Text = "Показать за последнюю неделю", Value = "7" });

                DateItems.Add(new SelectListItem { Text = "Показать за последний месяц", Value = "30"  });

                DateItems.Add(new SelectListItem { Text = "Показать за все время", Value = "999" });

                

                List<SelectListItem> CostEarnItems = new List<SelectListItem>();

                CostEarnItems.Add(new SelectListItem { Text = "Показать все", Value = "All" });

                CostEarnItems.Add(new SelectListItem { Text = "Показать только расходы", Value = "Расход" });

                CostEarnItems.Add(new SelectListItem { Text = "Показать только доходы", Value = "Доход" });
                #endregion
                ViewBag.DatePicker = DateItems;
                ViewBag.TypePicker = CostEarnItems;

                return View(ourOperations);
            }

            return View();
        }



        // GET: Operations/Create
        public ActionResult Create()
        {
            string currentUserId = User.Identity.GetUserId();
            var categories = db.Categories.Where(e => e.UserId.Equals(currentUserId));
            var accounts = db.Accounts.Where(e => e.UserId.Equals(currentUserId));
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.AccountId = new SelectList(accounts, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategoryId,AccountId,Amount")] Operation operation)
        {
            operation.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Operations.Add(operation);

                Account account = db.Accounts.Single(e => e.Id == operation.AccountId);
                Category category = db.Categories.Single(e => e.Id == operation.CategoryId);

                if (category.Type.Equals(cost))
                {
                    account.Amount -= operation.Amount;
                }
                else
                {
                    account.Amount += operation.Amount;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string currentUserId = User.Identity.GetUserId();
            var categories = db.Categories.Where(e => e.UserId.Equals(currentUserId));
            var accounts = db.Accounts.Where(e => e.UserId.Equals(currentUserId));
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.AccountId = new SelectList(accounts, "Id", "Name");
            return View(operation);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string currentUserId = User.Identity.GetUserId();
            Operation operation = db.Operations.Find(id);
            var categories = db.Categories.Where(e => e.UserId.Equals(currentUserId));
            var accounts = db.Accounts.Where(e => e.UserId.Equals(currentUserId));

            Account account = db.Accounts.Single(e => e.Id == operation.AccountId);
            Category category = db.Categories.Single(e => e.Id == operation.CategoryId);

            if (category.Type.Equals(cost))
            {
                account.Amount += operation.Amount;
            }
            else
            {
                account.Amount -= operation.Amount;
            }

            if (operation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            ViewBag.AccountId = new SelectList(accounts, "Id", "Name");
            return View(operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,AccountId,Amount,Date")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operation).State = EntityState.Modified;

                string currentUserId = User.Identity.GetUserId();
                var categories = db.Categories.Where(e => e.UserId.Equals(currentUserId));
                var accounts = db.Accounts.Where(e => e.UserId.Equals(currentUserId));

                Account account = db.Accounts.Single(e => e.Id == operation.AccountId);
                Category category = db.Categories.Single(e => e.Id == operation.CategoryId);

                if (category.Type.Equals(cost))
                {
                    account.Amount -= operation.Amount;
                }
                else
                {
                    account.Amount += operation.Amount;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(operation);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operation operation = db.Operations.Find(id);
            string currentUserId = User.Identity.GetUserId();
            var categories = db.Categories.Where(e => e.UserId.Equals(currentUserId));
            var accounts = db.Accounts.Where(e => e.UserId.Equals(currentUserId));

            Account account = db.Accounts.Single(e => e.Id == operation.AccountId);
            Category category = db.Categories.Single(e => e.Id == operation.CategoryId);

            ViewBag.Category = category.Name;
            ViewBag.Account = account.Name;
            if (operation == null)
            {
                return HttpNotFound();
            }
            return View(operation);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operation operation = db.Operations.Find(id);
            double amount = operation.Amount;
            Account account = db.Accounts.Single(e => e.Id == operation.AccountId);
            Category category = db.Categories.Single(e => e.Id == operation.CategoryId);
            if (category.Type.Equals(cost))
            {
                account.Amount += amount;
            }
            else
            {
                account.Amount -= amount;
            }
            db.Operations.Remove(operation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
