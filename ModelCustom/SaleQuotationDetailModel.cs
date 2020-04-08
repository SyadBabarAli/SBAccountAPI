namespace SBAccountAPI.ModelCustom
{
    public class SaleQuotationDetailModel
    {
        public int SaleQuotationId { get; set; }
        public int CompanyId { get; set; }
        public int SaleQuotationDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}