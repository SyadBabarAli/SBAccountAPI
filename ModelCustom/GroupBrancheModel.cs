using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Attributes;
using SBAccountAPI.Models;

namespace SBAccountAPI.ModelCustom
{
    //[MapsFrom(typeof(GroupBranch))]
    public class GroupBranchModel
    {
        public int CompanyId { get; set; }
        public int GroupBranchId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        //public int GroupBranchId { get; set; }
        //public int CompanyId { get; set; }
        //public string Name { get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsDeleted { get; set; }
        //public DateTime? Created { get; set; }
        //public DateTime? Modified { get; set; }
        //public string CreatedBy { get; set; }
        //public string ModifiedBy { get; set; }
    }
}