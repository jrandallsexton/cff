﻿
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CFF.Data
{
    public class CffContext : DbContext
    {
        public CffContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public CffContext(DbConnection connection) : base(connection, true) { }

        public DbSet<Forecast> Forecasts { get; set; }

        public DbSet<ForecastItem> ForecastItems { get; set; }

        public DbSet<ForecastResult> ForecastResults { get; set; }

        public DbSet<ForecastResultItem> ForecastResultItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Lookups/enumerations
            modelBuilder.Entity<Forecast>().Property(e => e.ForecastType).HasColumnName("ForecastTypeId");
            modelBuilder.Entity<ForecastItem>().Property(e => e.Type).HasColumnName("ForecastItemTypeId");
            modelBuilder.Entity<ForecastItem>().Property(e => e.Frequency).HasColumnName("FrequencyTypeId");
            modelBuilder.Entity<ForecastItem>().Property(e => e.DurationType).HasColumnName("DurationTypeId");

            //smalldatetime conversions
            modelBuilder.Entity<Forecast>().Property(t => t.Created).HasColumnType("smalldatetime");
            modelBuilder.Entity<Forecast>().Property(t => t.Begin).HasColumnType("smalldatetime");
            modelBuilder.Entity<Forecast>().Property(t => t.End).HasColumnType("smalldatetime");
            modelBuilder.Entity<ForecastItem>().Property(t => t.Begin).HasColumnType("smalldatetime");
            modelBuilder.Entity<ForecastItem>().Property(t => t.End).HasColumnType("smalldatetime");
            modelBuilder.Entity<ForecastResult>().Property(t => t.Created).HasColumnType("smalldatetime");

            // properties to ignore
            modelBuilder.Entity<ForecastItem>().Ignore(e => e.UId);
            modelBuilder.Entity<ForecastItem>().Ignore(e => e.Due);
        }
    }
}