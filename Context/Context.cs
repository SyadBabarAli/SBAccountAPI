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
        public virtual DbSet<SettingCustomerCategory> SettingCustomerCategories { get; set; }
        public virtual DbSet<SettingCurrency> SettingCurrencies { get; set; }
    }
}
