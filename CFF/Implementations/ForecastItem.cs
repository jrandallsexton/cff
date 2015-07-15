
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;

namespace CFF
{

    public class ForecastItem : IForecastItem
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public EForecastItemType Type { get; set; }
        public EFrequency Frequency { get; set; }
        public int StartDate { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Decimal Amount { get; set; }
        public EDurationType DurationType { get; set; }
        public int DurationValue { get; set; }

        public ForecastItem()
        {
            this.Id = Guid.NewGuid();
            this.End = DateTime.MaxValue;
        }

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, DateTime begin) : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Begin = begin;
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

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, DateTime begin, DateTime end)
            : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Begin = begin;
            this.End = end;
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