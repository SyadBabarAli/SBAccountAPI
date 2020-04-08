using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingFrequency
    {
        [Key, Column(Order = 1)] 
        public int SettingFrequencyId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}