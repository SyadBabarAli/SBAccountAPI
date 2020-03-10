using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingAccount
    {
        [Key]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}