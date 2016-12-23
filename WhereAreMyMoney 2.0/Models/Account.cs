using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereAreMyMoney_2._0.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }

        public string UserId { get; set; }
    }
}