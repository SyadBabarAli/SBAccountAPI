using System;

namespace SBAccountAPI.ModelCustom
{
    public class SaleQuotationModel
    {
        public int SaleQuotationId { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public string Number { get; set; }
        public DateTime? DateSale { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Reference { get; set; }
        public int BranchId { get; set; }
        public int? SalePersonId { get; set; }
        public int CurrencyId { get; set; }
        public string Term { get; set; }
        public int AttachedId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public int? SettingStatusId { get; set; }

        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string SalePersonName { get; set; }
        public string StatusName { get; set; }

        public decimal? GrossAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }

    }
}