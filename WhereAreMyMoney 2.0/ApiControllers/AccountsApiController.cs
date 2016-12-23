using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WhereAreMyMoney_2._0.Models;

namespace WhereAreMyMoney_2._0.ApiControllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountsApiController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();

        [HttpGet]
        [ActionName("getAccountById")]
        public HttpResponseMessage GetAccountById(string id)
        {
            try
            {
                int accountId = int.Parse(id);
                var userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    var account = db.Accounts.Single(e => e.Id == accountId);
                    return Request.CreateResponse(HttpStatusCode.OK, account);
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