
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
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Decimal Amount { get; set; }

        public ForecastItem()
        {
            this.Id = Guid.NewGuid();
        }

        public ForecastItem(string name, EForecastItemType type, EFrequency frequency, decimal amount, DateTime begin) : this()
        {
            this.Name = name;
            this.Type = type;
            this.Frequency = frequency;
            this.Amount = amount;
            this.Begin = begin;
        }

    }

}