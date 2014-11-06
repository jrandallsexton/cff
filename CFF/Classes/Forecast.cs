
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;
using CFF.Interfaces;

namespace CFF
{

    public class Forecast : IForecast 
    {

        private List<IForecastItem> _items = null;

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public EForecastType Type { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public Decimal AmountBegin { get; set; }

        public IEnumerable<IForecastItem> Items { get { return this._items; } }

        public Forecast()
        {
            this.Created = DateTime.Now;
            this._items = new List<IForecastItem>();
        }

        public Forecast(string name, EForecastType type, DateTime begin, int durationInDays) : this()
        {
            this.Name = name;
            this.Type = type;
            this.Begin = begin;
            if (this.Type == EForecastType.Snapshot) { this.End = this.Begin.AddDays(durationInDays); }
        }

        public void AddItem(IForecastItem item)
        {
            this._items.Add(item);
        }

    }

}