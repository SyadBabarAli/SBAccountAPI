using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SaleReturnModel
    {
        [Key, Column(Order = 1)] 
        public int CompanyId { get; set; }
        [Key, Column(Order = 2)] 
        public int SaleReturnId { get; set; }
        public int? SaleDeliveryId { get; set; }
        public string SaleDeliveryNumber { get; set; }
        public int? SaleInvoiceId { get; set; }
        public string SaleInvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string Number { get; set; }
        public DateTime? SaleReturnDate { get; set; }
        public string Reference { get; set; }
        public string Comment { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? ManualRoundOff { get; set; }
        public decimal? AutoRoundOff { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? AllocatedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string IsVoid { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public string AutoSettle { get; set; }
        public int? MasterGroupId { get; set; }
        public int? OrderBookerId { get; set; }
        public int? DeliveryPersonId { get; set; }
        public int? SalesmanId { get; set; }
        public int? QuantityCalculation { get; set; }
        public int? AccountId { get; set; }
        public int? Paymentreference { get; set; }
        public decimal? RefundAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SettingStatusId { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }

        public List<SaleReturnDetailModel> saleReturnDetailModel { get; set; }
        public List<SettingStatus> settingStatusModel { get; set; }
        public List<ListCustomer> listCustomerModel { get; set; }
    }
}