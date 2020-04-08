using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.ModelCustom
{
    public class SaleInvoiceModel
    {
        [Key, Column(Order = 1)] 
        public int CompanyId { get; set; }
        [Key, Column(Order = 2)] 
        public int SaleInvoiceId { get; set; }
        public string SaleQuotationId { get; set; }
        public string SaleQuotationNumber { get; set; }
        public string SaleOrderId { get; set; }
        public string SaleOrderNumber { get; set; }
        public string SaleOrderDate { get; set; }
        public string SaleDeliveryId { get; set; }
        public string SaleDeliveryNumber { get; set; }
        public int? CustomerId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public string Number { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate { get; set; }
        public string Reference { get; set; }
        public string AccountId { get; set; }
        public string PaymentReference { get; set; }
        public string Comments { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public bool? ShippingCharges { get; set; }
        public bool? ManualRoundOff { get; set; }
        public bool? AutoRoundOff { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? PaidAmount { get; set; }
        public bool? ReceivedAmount { get; set; }
        public int? BalanceAmount { get; set; }
        public bool? Status { get; set; }
        public string IsVoid { get; set; }
        public bool? OutstandingBalance { get; set; }
        public string MasterGroupId { get; set; }
        public string MasterGroup { get; set; }
        public string OrderBookerId { get; set; }
        public string OrderBooker { get; set; }
        public string DeliveryPersonId { get; set; }
        public string DeliveryPerson { get; set; }
        public string SalesmanId { get; set; }
        public string Salesman { get; set; }
        public string IsPosInvoice { get; set; }
        public string Time { get; set; }
        public string PosCashRegister { get; set; }
        public bool? ReturnAmount { get; set; }
        public int? UnAllocatedAmount { get; set; }
        public int? AdjustedAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public int? GroupBranchId { get; set; }
        public int? SalePersonId { get; set; }
        public int? SettingStatusId { get; set; }

        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string SalePersonName { get; set; }
        public string StatusName { get; set; }
        
        public List<SaleInvoiceDetailModel> saleInvoiceDetailModel { get; set; }
        public List<Models.GroupBranch> groupBranchModel { get; set; }
        public List<Models.ListSalePerson> salePersonModel { get; set; }
        public List<Models.SettingCurrency> currencyModel { get; set; }
        public List<Models.GeneralWarehouse> generalWarehouseModel { get; set; }
    }
}