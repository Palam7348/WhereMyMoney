using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;
using WhereAreMyMoney_2._0.Models;
using System.Data.Entity;
using System.Linq;
using WhereAreMyMoney_2._0.ApiControllers.HelpClasses;
using System.Collections.Generic;

namespace WhereAreMyMoney_2._0.ApiControllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesApiController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        [HttpGet]
        [ActionName("getCategoriesCost")]
        public HttpResponseMessage GetCategoriesCost()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var categoriesCost = db.Categories.Where(e => e.UserId.Equals(userId)).Where(e => e.Type.Equals("Расход")).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, categoriesCost);
                }
                
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
        }

        [HttpGet]
        [ActionName("getCategoriesEarn")]
        public HttpResponseMessage GetCategoriesEarn()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                
                if (userId != null)
                {
                    var categoriesEarn = db.Categories.Where(e => e.UserId.Equals(userId)).Where(e => e.Type.Equals("Доход")).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, categoriesEarn);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

        [Authorize]
        [HttpGet]
        [ActionName("getCategoryById")]
        public HttpResponseMessage GetCategoryById(string id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int categoryId = int.Parse(id);
                if (userId != null)
                {
                    var category = db.Categories.Single(e => e.Id == categoryId);
                    if (category!= null)
                        return Request.CreateResponse(HttpStatusCode.OK, category);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Authorize]
        [HttpGet]
        [ActionName("getCategoriesAndTheirCosts")]
        public HttpResponseMessage GetCategoriesAndTheirCosts()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var allOperations = db.Operations.Include(p => p.Category).Include(c => c.Account).ToList();

                    var ourOperations = allOperations.Where(e => e.Account.UserId.Equals(userId)).
                        Where(e => e.Category.UserId.Equals(userId)).ToList();

                    var categoriesCost = db.Categories.Where(e => e.UserId.Equals(userId)).Where(e => e.Type.Equals("Расход")).ToList();

                    List<CategoryCostPack> pack = new List<CategoryCostPack>();
                    foreach (var category in categoriesCost)
                    {
                        CategoryCostPack item = new CategoryCostPack();
                        item.Name = category.Name;
                        item.Amount = 0;
                        foreach (var operation in ourOperations)
                        {
                            if (category.Id == operation.CategoryId)
                            {
                                item.Amount += operation.Amount;
                            }
                        }
                        pack.Add(item);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, pack);

                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
