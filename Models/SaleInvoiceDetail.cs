using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBAccountAPI.Models
{
    public class SaleInvoiceDetail
    {
        [Key, Column(Order = 1)] public int CompanyId { get; set; }
        [Key, Column(Order = 2)] public int SaleInvoiceDetailId { get; set; }
        [Key, Column(Order = 3)] public int SaleInvoiceId { get; set; }
        public int? ProductId { get; set; }
        public int? AccountId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? MaximumRetailPrice { get; set; }
        public bool? IsMRPExclusiveTax { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? DiscountInPercent { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountInPrice { get; set; }
        public decimal? DiscountInAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string PackingDetail { get; set; }
        public string DetailAGroupId { get; set; }
        public string DetailAGroup { get; set; }
        public string DetailBGroupId { get; set; }
        public string DetailBGroup { get; set; }
        public string QuantityCalculation { get; set; }
        public string BatchId { get; set; }
        public string Batch { get; set; }
        public string SerialNumber { get; set; }
        public bool? IsDeleted { get; set; }
        public int? BranchId { get; set; }
        public int? SalePersonId { get; set; }
        public int? GeneralWarehouseId { get; set; }

        //[Key, Column(Order = 1)] 
        //public int CompanyId { get; set; }
        //[Key, Column(Order = 2)] 
        //public int SaleInvoiceDetailId { get; set; }
        //public int? SaleInvoiceId { get; set; }
        //public int ProductId { get; set; }
        //public int AccountId { get; set; }
        //public string Description { get; set; }
        //public int Quantity { get; set; }
        //public decimal? Price { get; set; }
        //public decimal? MaximumRetailPrice { get; set; }
        //public bool IsMRPExclusiveTax { get; set; }
        //public decimal? PurchasePrice { get; set; }
        //public decimal? DiscountInPercent { get; set; }
        //public decimal? GrossAmount { get; set; }
        //public decimal? TaxAmount { get; set; }
        //public decimal? DiscountAmount { get; set; }
        //public decimal? DiscountInPrice { get; set; }
        //public decimal? DiscountInAmount { get; set; }
        //public decimal? NetAmount { get; set; }
        //public string PackingDetail { get; set; }
        //public string DetailAGroupId { get; set; }
        //public string DetailAGroup { get; set; }
        //public string DetailBGroupId { get; set; }
        //public string DetailBGroup { get; set; }
        //public string QuantityCalculation { get; set; }
        //public string BatchId { get; set; }
        //public string Batch { get; set; }
        //public int WarehouseId { get; set; }
        //public string SerialNumber { get; set; }
        //public bool IsDeleted { get; set; }

    }
}