using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SBAccountAPI.Models
{
    public class SettingUnitOfMeasurment
    {
        [Key, Column(Order = 0)]
        public int UnitOfMeasurementId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int? Measure { get; set; }
        public bool? IsActive { get; set; }
    }
}