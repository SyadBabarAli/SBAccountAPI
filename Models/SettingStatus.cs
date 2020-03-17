using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingStatus
    {
        [Key, Column(Order = 0)]
        public int SettingStatusId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}