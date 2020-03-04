using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingCustomerCategory
    {
        [Key]
        public int CustomerCategoryId { get; set; }
        public int CompanyId { get; set; }
        public string CustomerCategory { get; set; }
    }
}