﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SaleQuotation
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int SaleQuotationId { get; set; }
        public int CustomerId { get; set; }
        public string Number { get; set; }
        public DateTime? DateSale { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Reference { get; set; }
        public int BranchId { get; set; }
        public int SalePersonId { get; set; }
        public int CurrencyId { get; set; }
        public string Term { get; set; }
        public int? AttachedId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public int? SettingStatusId { get; set; }


        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }

        public List<SaleQuotationDetail> saleQuotationDetail { get; set; }
    }
}