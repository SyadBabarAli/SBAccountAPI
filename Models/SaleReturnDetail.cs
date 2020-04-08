using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SaleReturnDetail
    {
        [Key, Column(Order = 1)] public int CompanyId { get; set; }
        [Key, Column(Order = 2)] public int SaleReturnDetailId { get; set; }
        public int SaleReturnId { get; set; }
        public int? ProductId { get; set; }
        public int? AccountId { get; set; } 
        public int? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? MaximumRetailPrice { get; set; }
        public string IsMrpExclusiveTax { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? DiscountInPercent { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountInPrice { get; set; }
        public decimal? DiscountInAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? DetailaGroupId { get; set; }
        public int? DetailbGroupId { get; set; }
        public int? QuantityCalculation { get; set; }
        public int? BatchId { get; set; }
        public int? WarehouseId { get; set; }
        public int? Serialnumber { get; set; }
        public bool? IsDeleted { get; set; }
    }
}