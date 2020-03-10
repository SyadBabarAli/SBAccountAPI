using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class GeneralTax
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int GeneralTaxId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int Rate { get; set; }
        public bool TaxPaidOnSale { get; set; }
        public bool TaxCollectedOnPurchase { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int AccountIdSale { get; set; }
        public int AccountIdPurchase { get; set; }
    }
}