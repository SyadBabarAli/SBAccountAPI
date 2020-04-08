using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models.Entities
{
    public class Todo : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public Employee Employee { get; set; }
    }
}