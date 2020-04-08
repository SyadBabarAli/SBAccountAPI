using SBAccountAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SBAccountAPI.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("name=SBAccount")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<SettingStatus> SettingStatuses { get; set; }

        public virtual DbSet<SettingBrand> SettingBrands { get; set; }
        public virtual DbSet<ListCustomer> ListCustomers { get; set; }
        public virtual DbSet<ListVendor> ListVendors { get; set; }
        public virtual DbSet<ListSalePerson> ListSalePersons { get; set; }

        public virtual DbSet<SettingCustomerCategory> SettingCustomerCategories { get; set; }
        public virtual DbSet<SettingCurrency> SettingCurrencies { get; set; }

        public virtual DbSet<CategoryCustomer> CategoryCustomers { get; set; }
        public virtual DbSet<CategoryVendor> CategoryVendors { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        public virtual DbSet<CategoryBrand> CategoryBrands { get; set; }
        public virtual DbSet<CategoryDepartment> CategoryDepartments { get; set; }
        public virtual DbSet<CategoryDesignation> CategoryDesignations { get; set; }

        public virtual DbSet<SettingParentProductCategory> SettingParentProductCategories { get; set; }
        //public virtual DbSet<SettingAccount> SettingAccounts { get; set; }
        public virtual DbSet<SettingCountry> SettingCountries { get; set; }


        public virtual DbSet<GroupBranch> GroupBranches { get; set; }


        public virtual DbSet<GeneralTax> GeneralTaxes { get; set; }
        public virtual DbSet<GeneralWarehouse> GeneralWarehouses { get; set; }


        public virtual DbSet<SaleGeographyRegion> SaleGeographyRegions { get; set; }
        public virtual DbSet<SaleGeographyZone> SaleGeographyZones { get; set; }
        public virtual DbSet<SaleGeographyTerritory> SaleGeographyTerritories { get; set; }
        public virtual DbSet<SaleGeographyArea> SaleGeographyAreas { get; set; }
        public virtual DbSet<SaleGeographySubArea> SaleGeographySubAreas { get; set; }

        public virtual DbSet<SaleQuotation> SaleQuotations { get; set; }
        public virtual DbSet<ListProduct> ListProducts { get; set; }

        public DbSet<SaleQuotationDetail> SaleQuotationDetails { get; set; }

        public virtual DbSet<SettingUnitOfMeasurment> SettingUnitOfMeasurments { get; set; }
        public virtual DbSet<SettingInventoryAccount> SettingInventoryAccounts { get; set; }
        public virtual DbSet<SettingAccount> SettingAccounts { get; set; }
        public virtual DbSet<SettingSaleAccount> SettingSaleAccounts { get; set; }

        public virtual DbSet<CategoryProductPrinciple> CategoryProductPrinciples { get; set; }
        public virtual DbSet<SettingProductType> SettingProductTypes { get; set; }

        public virtual DbSet<SaleInvoice> SaleInvoices { get; set; }
        public virtual DbSet<SaleInvoiceDetail> SaleInvoiceDetails { get; set; }

        public virtual DbSet<SaleRecurringInvoice> SaleRecurringInvoices { get; set; }
        public virtual DbSet<SaleRecurringInvoiceDetail> SaleRecurringInvoiceDetails { get; set; }

        public virtual DbSet<SettingFrequency> SettingFrequences { get; set; }

        public virtual DbSet<SaleReturn> SaleReturns { get; set; }
        public virtual DbSet<SaleReturnDetail> SaleReturnDetails { get; set; }

        //public virtual DbSet<SaleReceiveMoney> SaleReceiveMones { get; set; }
        //public virtual DbSet<SaleReceiveMoneyDetail> SaleReceiveMoneyDetails { get; set; }

        public virtual DbSet<SettingPaymentMode> SettingPaymentMode { get; set; }

        public virtual DbSet<SaleRefund> SaleRefunds { get; set; }
        public virtual DbSet<SaleRefundDetail> SaleRefundDetails { get; set; }
    }
}

