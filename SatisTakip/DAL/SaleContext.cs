using SatisTakip.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SatisTakip.DAL
{
    public class SaleContext : DbContext
    {
        public SaleContext() : base("SatisTakipDb") { }

        public DbSet<ArventoSale> Sales { get; set; }

        public DbSet<TurkcellSale> TurkcellSales { get; set; }

        public DbSet<logMail> MailLogs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }
}