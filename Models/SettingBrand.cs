using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingBrand
    {
        [Key]
        public int BrandId { get; set; }
        public int CompanyId { get; set; }
        public string BrandName { get; set; }
    }
}