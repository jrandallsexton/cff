
using System;
using System.Collections.Generic;

using CFF.Enumerations;
using CFF.Interfaces;

namespace CFF
{

    public class Forecast : IForecast 
    {

        private List<ForecastItem> _items = null;

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public EForecastType ForecastType { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public decimal AmountBegin { get; set; }

        public virtual IEnumerable<ForecastItem> Items { get { return this._items; } }

        public Forecast()
        {
            this.Created = DateTime.Now;
            this._items = new List<ForecastItem>();
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
            this._items.Add(item);
        }

    }

}