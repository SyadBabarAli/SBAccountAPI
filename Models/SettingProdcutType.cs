using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBAccountAPI.Models
{
    public class SettingProductType
    {
        [Key, Column(Order = 1)]
        public int SettingProductTypeId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}