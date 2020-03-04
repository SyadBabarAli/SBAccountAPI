using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingCurrency
    {
        [Key]
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}