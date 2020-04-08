using System;

namespace SBAccountAPI.ModelCustom
{
    public class SalePersonModel
    {
        public int SalePersonId { get; set; }
        public int CompanyId { get; set; }
        public string SalePersonName { get; set; }
        public bool? IsOrderBooker { get; set; }
        public bool? IsDelievery { get; set; }
        public bool? IsSaleMan { get; set; }
    }
}