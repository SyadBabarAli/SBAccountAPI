using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SaleGeographyArea
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int SaleGeographyRegionId { get; set; }
        [Key, Column(Order = 2)]
        public int SaleGeographyZoneId { get; set; }
        [Key, Column(Order = 3)]
        public int SaleGeographyTerritoryId { get; set; }
        [Key, Column(Order = 4)]
        public int SaleGeographyAreaId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}