using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class ListCustomer
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string DisplayName { get; set; }

        public string CustomerName { get; set; }
        public string PrintName { get; set; }
        public int CustomerCategoryId { get; set; }
        public int OpeningBalance { get; set; }
        public int CreditLimitAmount { get; set; }
        public int CreditLimitDays { get; set; }
        public int Discount { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CurrencyId { get; set; }
    }
}