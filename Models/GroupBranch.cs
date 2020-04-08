using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SBAccountAPI.Models.Entities;

namespace SBAccountAPI.Models
{
    public class GroupBranch 
    {
        [Key, Column(Order = 1)] 
        public int GroupBranchId { get; set; }
        [Key, Column(Order = 2)] 
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}