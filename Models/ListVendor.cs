using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class ListVendor
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string PrintName { get; set; }
        public string DisplayName { get; set; }
        public int OpeningBalance { get; set; }
        public int CreditLimitDays { get; set; }
        public int IsPrinciple { get; set; }
        public int VendorCategoryId { get; set; }
        public int CurrencyId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}