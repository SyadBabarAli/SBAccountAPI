using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBAccountAPI.Models
{
    public class SettingParentProductCategory
    {
        [Key, Column(Order = 0)]
        public int ParentProductCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}