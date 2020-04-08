using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Models
{
    public class SettingAccount
    {
        [Key]
        public int SettingAccountId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public string AccountClass { get; set; }
        public string AccountGroup { get; set; }
        public string Systemaccount { get; set; }
        public int? ParentAccountId { get; set; }
        public int? Order { get; set; }
        public int? CurrencyId { get; set; }
        public string Currency { get; set; }
        public string ChildAccount { get; set; }
        public string Bankaccount { get; set; }
        public bool? Trackdepreciation { get; set; }
        public decimal Balance { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string Createdby { get; set; }
        public DateTime? Modified { get; set; }
        public string Modifiedby { get; set; }
    }
}