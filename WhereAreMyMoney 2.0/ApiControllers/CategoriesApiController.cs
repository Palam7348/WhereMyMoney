using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Web.Http.Cors;
using WhereAreMyMoney_2._0.Models;
using System.Data.Entity;
using System.Linq;


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
    }
}
