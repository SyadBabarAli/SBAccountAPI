using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Attributes;
using SBAccountAPI.Models;

namespace SBAccountAPI.ModelCustom
{
    public class SaleReceiveMoneyDetailModel
    {
        [Key, Column(Order = 1)] public int CompanyId { get; set; }
        [Key, Column(Order = 2)] public int SaleReceiveMoneyDetailId { get; set; }
        public int SaleReceiveMoneyId { get; set; }

        public int? CustomerPaymentId { get; set; }
        public int? PaymentModeId { get; set; }
        public int? Reference { get; set; }
        public decimal? Amount { get; set; }
        public int? AccountId { get; set; }
        public int? InstrumentId { get; set; }
        public int? Instrument { get; set; }
        public int? BankName { get; set; }
        public int? MasterGroupCode { get; set; }
        public int? MasterGroupName { get; set; }
        public int? InstrumentNumber { get; set; }
        public DateTime? InstrumentDate { get; set; }
        public int? DetailAGroupId { get; set; }
        public int? DetailBGroupId { get; set; }
        public int? InstrumentStatus { get; set; }
        public bool? IsDeleted { get; set; }

        public string PaymentModeName { get; set; }
        public string AccountName { get; set; }
    }
}