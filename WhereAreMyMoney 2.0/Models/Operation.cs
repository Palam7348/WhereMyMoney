using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereAreMyMoney_2._0.Models
{
    public class Operation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int AccountId { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$")]
        public int Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Category Category { get; set; }

        public Account Account { get; set; }
    }
}