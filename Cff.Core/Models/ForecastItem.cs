
using Cff.Core.Enumerations;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cff.Core.Models
{
    public class ForecastItem : Entity<int>
    {

        public Guid UId { get; set; }

        public int ForecastId { get; set; }

        public string Name { get; set; }

        public EForecastItemType Type { get; set; }

        public EFrequency Frequency { get; set; }

        public int StartDate { get; set; }

        public DateTime Begin { get; set; }

        public DateTime? End { get; set; }

        public DateTime? LastProcessed { get; set; }

        public DateTime Due { get; set; }

        public decimal Amount { get; set; }

        public EDurationType DurationType { get; set; }

        public int DurationValue { get; set; }

        public ForecastItem()
        {
            this.UId = Guid.NewGuid();
        }

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, DateTime begin, DateTime? lastProcessed)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Begin = begin;
            this.LastProcessed = lastProcessed;
        }

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, int firstDayOfMonth)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.StartDate = firstDayOfMonth;
            if (firstDayOfMonth < DateTime.Now.Day)
            {
                var nextMonth = DateTime.Now.AddMonths(1);
                this.Begin = new DateTime(nextMonth.Year, nextMonth.Month, firstDayOfMonth);
            }
            else
            {
                this.Begin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, firstDayOfMonth);
            }
        }

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, DateTime begin, DateTime end, DateTime? lastProcessed)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Begin = begin;
            this.End = end;
            this.LastProcessed = lastProcessed;
        }

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, DateTime begin, EDurationType durationType, int durationValue)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Begin = begin;
            this.DurationType = durationType;
            this.DurationValue = durationValue;
        }

        public class EntityConfiguration : IEntityTypeConfiguration<ForecastItem>
        {
            public void Configure(EntityTypeBuilder<ForecastItem> builder)
            {
                builder.ToTable("ForecastItem");

                //modelBuilder.Entity<ForecastItem>().Property(e => e.Type).HasColumnName("ForecastItemTypeId");
                //modelBuilder.Entity<ForecastItem>().Property(e => e.Frequency).HasColumnName("FrequencyTypeId");
                //modelBuilder.Entity<ForecastItem>().Property(e => e.DurationType).HasColumnName("DurationTypeId");

                //modelBuilder.Entity<ForecastItem>().Property(t => t.Begin).HasColumnType("smalldatetime");
                //modelBuilder.Entity<ForecastItem>().Property(t => t.End).HasColumnType("smalldatetime");

                // properties to ignore
                //modelBuilder.Entity<ForecastItem>().Ignore(e => e.UId);
                //modelBuilder.Entity<ForecastItem>().Ignore(e => e.Due);
            }
        }
    }
}