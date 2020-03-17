using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SaleQuotationDetail
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int SaleQuotationId { get; set; }
        [Key, Column(Order = 2)]
        public int SaleQuotationDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Amount { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        //public string ProductDesc { get; set; }


    }
}