using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class ListProduct
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int ProductTypeId { get; set; }
        public int PurchasePrice { get; set; }
        public int SalePrice { get; set; }
        public int RetailPrice { get; set; }
        public int IsPurchaseFractional { get; set; }
        public bool IsTrackInventory { get; set; }
    }
}