using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SBAccountAPI.Models.Entities;

namespace SBAccountAPI.Models
{
    public class SaleReceiveMoneyDetail
    {

        //[Key, Column(Order = 2)]
        //public int SaleReceiveMoneyId { get; set; }
        ////[Key, Column(Order = 3)]
        //public int SaleReceiveMoneyDetailId { get; set; }


        [Key, Column(Order = 1)] public int CompanyId { get; set; }
        [Key, Column(Order = 2)] 
        public int SaleReceiveMoneyDetailId { get; set; }
        //[Key, Column(Order = 3)] 
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


        //[Key, Column(Order = 1)] 
        //public int CompanyId { get; set; }
        //[Key, Column(Order = 2)]
        //public int SaleReceiveMoneyDetailId { get; set; }
        //[Key, Column(Order = 3)] 
        //public int SaleReceiveMoneyId { get; set; }

        //public int? CustomerPaymentId { get; set; }
        //public int? PaymentModeId { get; set; }
        //public int? Reference { get; set; }
        //public decimal? Amount { get; set; }
        //public int? AccountId { get; set; }
        //public int? InstrumentId { get; set; }
        //public int? Instrument { get; set; }
        //public int? BankName { get; set; }
        //public int? MasterGroupCode { get; set; }
        //public int? MasterGroupName { get; set; }
        //public int? InstrumentNumber { get; set; }
        //public DateTime? InstrumentDate { get; set; }
        //public int? DetailAGroupId { get; set; }
        //public int? DetailBGroupId { get; set; }
        //public int? InstrumentStatus { get; set; }
        //public bool? IsDeleted { get; set; }
    }
}