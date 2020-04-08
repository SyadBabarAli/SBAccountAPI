using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SBAccountAPI.Models.Entities;

namespace SBAccountAPI.Models
{
    public class SettingPaymentMode
    {
        [Key, Column(Order = 1)] 
        public int SettingPaymentModeId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}