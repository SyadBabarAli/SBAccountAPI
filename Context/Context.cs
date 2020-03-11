using SBAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Context
{
    public class Context : DbContext
    {
        public Context() : base("name=SBAccount")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<SettingBrand> SettingBrands { get; set; }
        public virtual DbSet<ListCustomer> ListCustomers { get; set; }
        public virtual DbSet<ListVendor> ListVendors { get; set; }
        public virtual DbSet<SettingCustomerCategory> SettingCustomerCategories { get; set; }
        public virtual DbSet<SettingCurrency> SettingCurrencies { get; set; }

        public virtual DbSet<CategoryCustomer> CategoryCustomers { get; set; }
        public virtual DbSet<CategoryVendor> CategoryVendors { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        public virtual DbSet<CategoryBrand> CategoryBrands { get; set; }
        public virtual DbSet<CategoryDepartment> CategoryDepartments { get; set; }
        public virtual DbSet<CategoryDesignation> CategoryDesignations { get; set; }

        public virtual DbSet<SettingParentProductCategory> SettingParentProductCategories { get; set; }
        public virtual DbSet<SettingAccount> SettingAccounts { get; set; }
        public virtual DbSet<SettingCountry> SettingCountries { get; set; }


        public virtual DbSet<GroupBranch> GroupBranches { get; set; }
        public virtual DbSet<SaleQuotation> SaleQuotationes { get; set; }


        public virtual DbSet<GeneralTax> GeneralTaxes { get; set; }
        public virtual DbSet<GeneralWarehouse> GeneralWarehouses { get; set; }


        public virtual DbSet<SaleGeographyRegion> SaleGeographyRegions { get; set; }
        public virtual DbSet<SaleGeographyZone> SaleGeographyZones { get; set; }
        public virtual DbSet<SaleGeographyTerritory> SaleGeographyTerritories { get; set; }
        public virtual DbSet<SaleGeographyArea> SaleGeographyAreas { get; set; }
        public virtual DbSet<SaleGeographySubArea> SaleGeographySubAreas { get; set; }

        public virtual DbSet<SaleQuotation> SaleQuotations { get; set; }
    }
}
