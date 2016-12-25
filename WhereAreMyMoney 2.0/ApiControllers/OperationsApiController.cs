using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using WhereAreMyMoney_2._0.Models;
using System.Data.Entity;

namespace WhereAreMyMoney_2._0.ApiControllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OperationsApiController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();
        private const string undefined = "undefined";

        [HttpGet]
        [ActionName("getOperationsByCategoryId")]
        public HttpResponseMessage GetOperationsByCategoryId(string id)
        {
            try
            {
                int categoryId = int.Parse(id);
                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var operations = db.Operations.Where(e => e.CategoryId == categoryId).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, operations);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet]
        [ActionName("getOperationsByAccountId")]
        public HttpResponseMessage GetOperationsByAccountId(string id)
        {
            try
            {
                int accountId = int.Parse(id);
                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var operations = db.Operations.Where(e => e.AccountId == accountId).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, operations);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ActionName("getOperationsBySeveralParamentres")]
        public HttpResponseMessage GetOperationsBySeveralParamentres(string id)
        {
            try
            {
                int categoryId;
                int accountId;
                int dateChoiseId;
                string type = undefined;
                List<string> parametres = id.Split(',').ToList();

                int.TryParse(parametres[0], out categoryId);
                int.TryParse(parametres[1], out accountId);
                ;

                if (!parametres[3].Equals(undefined))
                {
                    type = parametres[3];
                }

                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var allOperations = db.Operations.Include(p => p.Category).Include(c => c.Account).ToList();

                    var ourOperations = allOperations.Where(e => e.Account.UserId.Equals(userId)).
                        Where(e => e.Category.UserId.Equals(userId)).ToList();

                    if (int.TryParse(parametres[0], out categoryId))
                    {
                        ourOperations = ourOperations.Where(e => e.CategoryId == categoryId).ToList();
                    }
                    if (int.TryParse(parametres[1], out accountId))
                    {
                        ourOperations = ourOperations.Where(e => e.AccountId == accountId).ToList();
                    }
                    if (int.TryParse(parametres[2], out dateChoiseId))
                    {
                        DateTime now = DateTime.Now;
                        switch (dateChoiseId)
                        {
                            
                            case 3:
                                DateTime threeDaysBefore = DateTime.Now.AddDays(-3);
                                ourOperations = ourOperations.Where(e => e.Date > threeDaysBefore).ToList();
                                break;
                            case 7:
                                DateTime weekBefore = DateTime.Now.AddDays(-7);
                                ourOperations = ourOperations.Where(e => e.Date > weekBefore).ToList();
                                break;
                            case 30:
                                DateTime monthBefore = DateTime.Now.AddDays(-30);
                                ourOperations = ourOperations.Where(e => e.Date > monthBefore).ToList();
                                break;
                            case 999:
                                break;
                        }
                    }
                    if (type.Equals("Расход"))
                    {
                        ourOperations = ourOperations.Where(e => e.Category.Type.Equals(type)).ToList();
                    }
                    if (type.Equals("Доход"))
                    {
                        ourOperations = ourOperations.Where(e => e.Category.Type.Equals(type)).ToList();
                    }
                    if (type.Equals("All"))
                    {
                        ourOperations = ourOperations.Where(e => e.Category.Type.Equals(type)).ToList();
                    }
                    ourOperations.Reverse();
                    return Request.CreateResponse(HttpStatusCode.OK, ourOperations);
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
