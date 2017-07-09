
using System;

using CFF.Enumerations;

namespace CFF
{

    public class ForecastItem : IForecastItem
    {

        public int Id { get; set; }

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
            //this.End = DateTime.MaxValue;
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

    }

}