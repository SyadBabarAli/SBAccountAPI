using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class ListSalePerson
    {
        [Key, Column(Order = 0)]
        public int CompanyId { get; set; }
        [Key, Column(Order = 1)]
        public int SalePersonId { get; set; }
        public string SalePersonName { get; set; }
        public bool IsOrderBooker { get; set; }
        public bool IsDelievery { get; set; }
        public bool IsSaleMan { get; set; }
    }
}