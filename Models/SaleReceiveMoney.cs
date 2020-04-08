using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SBAccountAPI.Models.Entities;

namespace SBAccountAPI.Models
{
    public class SaleReceiveMoney 
    {
        [Key, Column(Order = 1)] public int SaleReceiveMoneyId { get; set; }
        [Key, Column(Order = 2)] public int CompanyId { get; set; }
        public int? CustomerId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Number { get; set; }
        public DateTime? ReceiveMoneyDate { get; set; }
        public string Reference { get; set; }
        public string Comment { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public int? SettingStatusId { get; set; }
        public bool? IsVoid { get; set; }
        public bool? AutoSettle { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public int? MasterGroupid { get; set; }
        public int? OrderbookerId { get; set; }
        public int? DeliveryPersonId { get; set; }
        public int? SalesmanId { get; set; }
        public bool? IsDeleted { get; set; }
        public List<SaleReceiveMoneyDetail> SaleReceiveMoneyDetail { get; set; }

    }
}