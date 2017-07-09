
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Forecast>()
                .Property(e => e.ForecastType).HasColumnName("ForecastTypeId");

            modelBuilder.Entity<ForecastItem>().Ignore(e => e.UId);

            modelBuilder.Entity<ForecastItem>().Ignore(e => e.LastProcessed);
            modelBuilder.Entity<ForecastItemWorkspace>().Ignore(e => e.Due);

            modelBuilder.Entity<ForecastItem>().Property(e => e.Type).HasColumnName("ForecastItemTypeId");
            modelBuilder.Entity<ForecastItem>().Property(e => e.Frequency).HasColumnName("FrequencyTypeId");
            modelBuilder.Entity<ForecastItem>().Property(e => e.DurationType).HasColumnName("DurationTypeId");
        }
    }
}