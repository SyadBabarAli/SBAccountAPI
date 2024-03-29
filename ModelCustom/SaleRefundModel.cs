﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.ModelCustom
{
    public class SaleRefundModel
    {
        [Key, Column(Order = 1)] public int SaleRefundId { get; set; }
        [Key, Column(Order = 2)] public int CompanyId { get; set; }
        public int? CustomerId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public string Reference { get; set; }
        public string Comment { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public int? Status { get; set; }
        public string IsVoid { get; set; }
        public string AutoSettle { get; set; }
        public int? MasterGroupId { get; set; }
        public int? MasterGroup { get; set; }
        public int? OrderBookerId { get; set; }
        public int? OrderBooker { get; set; }
        public int? DeliveryPersonId { get; set; }
        public int? DeliveryPerson { get; set; }
        public int? SalesmanId { get; set; }
        public int? Salesman { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public bool? IsDeleted { get; set; }

        public string CustomerName { get; set; }
        public string StatusName { get; set; }

        public List<SaleRefundDetailModel> saleRefundDetailModel { get; set; }
        public List<Models.SettingPaymentMode> settingPaymentMode { get; set; }
        public List<Models.SettingStatus> settingStatus { get; set; }


        //public string CustomerName { get; set; }
        //public string BranchName { get; set; }
        //public string SalePersonName { get; set; }
        //public string StatusName { get; set; }

        //public List<SaleInvoiceDetailModel> saleInvoiceDetailModel { get; set; }
        //public List<Models.GroupBranch> groupBranchModel { get; set; }
        //public List<Models.ListSalePerson> salePersonModel { get; set; }
        //public List<Models.SettingCurrency> currencyModel { get; set; }
        //public List<Models.GeneralWarehouse> generalWarehouseModel { get; set; }
    }
}