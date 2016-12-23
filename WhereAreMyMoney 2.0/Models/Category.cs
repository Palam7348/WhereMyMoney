using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhereAreMyMoney_2._0.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }
        [Required]
        public string Name { get; set; }

        public string Type { get; set; }
    }
}