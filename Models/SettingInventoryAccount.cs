using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingInventoryAccount
    {
        [Key, Column(Order = 0)]
        public int SettingInventoryAccountId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string Createdby { get; set; }
        public DateTime? Modified { get; set; }
        public string Modifiedby { get; set; }
    }
}