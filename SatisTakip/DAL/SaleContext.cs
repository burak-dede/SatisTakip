using SatisTakip.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SatisTakip.DAL
{
    public class SaleContext : DbContext
    {
        public SaleContext() : base("SatisTakipDb") { }

        public DbSet<CompanyOneSale> Sales { get; set; }

        public DbSet<CompanyTwoSale> CompanyTwoSale { get; set; }

        public DbSet<logMail> MailLogs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }
}