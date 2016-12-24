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
        [RegularExpression(@"[\d]{1,4}([.,][\d]{1,2})?")]
        public double Amount { get; set; }

        public string UserId { get; set; }
    }
}