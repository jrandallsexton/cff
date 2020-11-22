using Cff.Core.Implementations;
using Cff.Core.Models;

using Microsoft.EntityFrameworkCore;

namespace Cff.Core.Data
{
    public class CffContext : DbContext
    {
        public CffContext(DbContextOptions<CffContext> options) :
            base(options) { }

        public DbSet<Forecast> Forecasts { get; set; }

        public DbSet<ForecastItem> ForecastItems { get; set; }

        public DbSet<ForecastResult> ForecastResults { get; set; }

        public DbSet<ForecastResultItem> ForecastResultItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ForecastResult>().Property(t => t.Created).HasColumnType("smalldatetime");
        }
    }
}