using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBAccountAPI.Models
{
    public class ListProduct
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Symbol { get; set; }
        public string Precision { get; set; }
        public bool? FractionalUnit { get; set; }
        public bool? TrackInventory { get; set; }
        public int? InventoryAccountId { get; set; }
        public bool? IsForSale { get; set; }
        public decimal? SalePrice { get; set; }
        public int? SaleAccountId { get; set; }
        public int? SaleDiscountAccountId { get; set; }
        public string SaleTax { get; set; }
        public bool? IsForPurchase { get; set; }
        public decimal PurchasePrice { get; set; }
        public int? ExpenseAccountId { get; set; }
        public int? PurchaseDiscountAccountId { get; set; }
        public string PurchaseTax { get; set; }
        public decimal MaximumRetailPrice { get; set; }
        public bool? IsMRPExclusiveTax { get; set; }
        public string ImageUrl { get; set; }
        public string Base64imagestring { get; set; }
        public int? BaseProductId { get; set; }
        public decimal ConversionFactor { get; set; }
        public bool? HasCalculationfield { get; set; }
        public decimal AverageCost { get; set; }
        public bool? HasBatch { get; set; }
        public bool? IsOpening { get; set; }
        public bool? HasSerialnumber { get; set; }
        public int? MinimumStockLevel { get; set; }
        public int? UnitOfMeasurementId { get; set; }

        public int? CategoryProductId { get; set; }
        public int? CategoryProductPrincipleId { get; set; }
        public int? CategoryBrandId { get; set; }
        public int? HSCodeId { get; set; }
        public int? SettingProductTypeId { get; set; }

        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}