using System;

namespace SBAccountAPI.ModelCustom
{
    public class CurrencyModel
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}