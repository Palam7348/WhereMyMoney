using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereAreMyMoney_2._0.Models
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }

        public int AccountFirstId { get; set; }

        public int AccountSecondId { get; set; }

        public int Amount { get; set; }
    }
}