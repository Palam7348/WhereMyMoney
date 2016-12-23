using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace WhereAreMyMoney_2._0.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Categories = new List<Category>();
            Accounts = new List<Account>();
        }

        public int Year { get; set; }

        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}