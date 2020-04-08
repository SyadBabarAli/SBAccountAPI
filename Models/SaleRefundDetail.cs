using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBAccountAPI.Models
{
    public class SaleRefundDetail
    {
        [Key, Column(Order = 1)] public int SaleRefundId { get; set; }
        [Key, Column(Order = 2)] public int SaleRefundDetailId { get; set; }
        public int CompanyId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? PaymentModeId { get; set; }
        public string DocumentNumber { get; set; }
        public decimal? Amount { get; set; }
        public int? AccountId { get; set; }
        public int? DetailAGroupId { get; set; }
        public int? DetailBGroupId { get; set; }
    }
}