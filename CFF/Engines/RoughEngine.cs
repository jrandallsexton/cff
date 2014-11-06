
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;
using CFF.Interfaces;

namespace CFF.Engines
{

    public class RoughEngine : IForecastEngine 
    {

        private TimeSpan _defaultMaxForecastPeriod = new TimeSpan(365, 0, 0, 0);
        private IForecastHelper _helper = null;

        public IForecastResult CreateForecast(IForecastHelper helper, IForecast forecast)
        {

            this._helper = helper;

            ForecastResult result = new ForecastResult(forecast.AmountBegin);

            // If the forecast type is indefinite, ensure that the ending date isn't greater than one year out
            if ((forecast.Type == EForecastType.Indefinite) && (forecast.End > forecast.Begin.AddYears(1))) {
                forecast.End = forecast.Begin.Add(this._defaultMaxForecastPeriod); }

            // get a collection of every day in the period with all of the items in each day that need to be processed
            IDictionary<DateTime, IList<IForecastItem>> values = this.LoadItems(this._helper, forecast);

            DateTime idx = forecast.Begin;
            decimal amtBegin = forecast.AmountBegin;
            decimal amtEnd = Decimal.MinValue;

            while (idx <= forecast.End)
            {

                // process items that need processing
                if ((values.ContainsKey(idx)) && (values[idx].Count > 0))
                {
                    // we found items that needed to be processed on the current day
                    foreach (var item in values[idx])
                    {
                        if (item.Type == EForecastItemType.Income)
                        {
                            amtBegin += item.Amount;
                        }
                        else
                        {
                            amtBegin -= item.Amount;
                        }
                    }
                    
                }

                // store the daily result
                result.Results.Add(idx, new ForecastResultItem(amtBegin, amtEnd));

                amtBegin = amtEnd;

                idx = idx.AddDays(1d);
            }

            return result;
        }

        private IDictionary<DateTime, IList<IForecastItem>> LoadItems(IForecastHelper helper, IForecast forecast)
        {

            // 1A. Create workspace holders for each forecast item; will be used to determine what days each item will affect the forecast
            List<ForecastItemWorkspace> workspaces = new List<ForecastItemWorkspace>();

            // 1B. We will also cache the forecast items so that we can retrieve them by the key
            Dictionary<Guid, IForecastItem> fcItemCache = new Dictionary<Guid, IForecastItem>();

            foreach (IForecastItem item in forecast.Items) {
                workspaces.Add(new ForecastItemWorkspace(item, DateTime.MaxValue, DateTime.MinValue));
                fcItemCache.Add(item.Id, item);
            }

            // 2. Prepopulate a collection representing every day within the forecast window (duration of the forecast)
            IDictionary<DateTime, IList<IForecastItem>> values = new Dictionary<DateTime, IList<IForecastItem>>();

            DateTime idx = forecast.Begin;

            while (idx <= forecast.End)
            {
                values.Add(idx, new List<IForecastItem>());
                idx = idx.AddDays(1d);
            }

            // 3. Iterate through each workspace, determine each day it needs to be proccessed and add it to the return collection
            for (var i = 0; i < workspaces.Count; i++)
            {
                var ws = workspaces[i];

                // reset the counter as we will be iterating every day within the forecast period again
                idx = forecast.Begin;

                while (idx <= forecast.End)
                {
                    if (ws.Due.CompareTo(idx) == 0)
                    {
                        values[idx].Add(fcItemCache[ws.Id]);
                        ws.LastProcessed = idx;
                        ws.Due = helper.GetDueDate(ws.LastProcessed, ws.Frequency);
                    }
                    idx = idx.AddDays(1d);
                }
            }

            return values;
        }

    }

    class ForecastItemWorkspace : ForecastItem
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