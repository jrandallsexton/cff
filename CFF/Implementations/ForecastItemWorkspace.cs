using System;

namespace CFF
{
    public class ForecastItemWorkspace : ForecastItem
    {

        public DateTime LastProcessed { get; set; }
        public DateTime Due { get; set; }

        public ForecastItemWorkspace(IForecastItem forecastItem, DateTime lastProcessed, DateTime due)
        {
            this.Id = forecastItem.Id;
            this.Amount = forecastItem.Amount;
            this.Begin = forecastItem.Begin;
            this.End = forecastItem.End;
            this.Frequency = forecastItem.Frequency;
            this.Name = forecastItem.Name;
            this.Type = forecastItem.Type;
            this.LastProcessed = lastProcessed;
            this.Due = due;
        }

    }
}