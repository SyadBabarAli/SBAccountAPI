using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingCountry
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}