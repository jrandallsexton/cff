
using Cff.Core.Enumerations;
using Cff.Core.Implementations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;

namespace Cff.Core.Models
{
    public class Forecast : Entity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public EForecastType ForecastType { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public decimal AmountBegin { get; set; }

        public virtual ICollection<ForecastItem> Items { get; set; }

        public Forecast()
        {
            this.Created = DateTime.Now;
            this.Items = new List<ForecastItem>();
        }

        public Forecast(string name, EForecastType type, DateTime begin, int durationInDays) : this()
        {
            this.Name = name;
            this.ForecastType = type;
            this.Begin = begin;
            if (this.ForecastType == EForecastType.Snapshot) { this.End = this.Begin.AddDays(durationInDays); }
        }

        public void AddItem(ForecastItem item)
        {
            this.Items.Add(item);
        }

        public class EntityConfiguration : IEntityTypeConfiguration<Forecast>
        {
            public void Configure(EntityTypeBuilder<Forecast> builder)
            {
                builder.ToTable("Forecast");
                //modelBuilder.Entity<Forecast>().Property(e => e.ForecastType).HasColumnName("ForecastTypeId");
                //modelBuilder.Entity<Forecast>().Property(t => t.Created).HasColumnType("smalldatetime");
                //modelBuilder.Entity<Forecast>().Property(t => t.Begin).HasColumnType("smalldatetime");
                //modelBuilder.Entity<Forecast>().Property(t => t.End).HasColumnType("smalldatetime");
            }
        }
    }
}