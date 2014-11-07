
using System;
using System.Collections.Generic;

using CFF.Enumerations;
using CFF.Interfaces;

namespace CFF
{

    public class ForecastHelper : IForecastHelper
    {

        private bool _verbose = false;

        public DateTime GetDueDate(DateTime lastProcessed, EFrequency frequency)
        {
            switch (frequency)
            {
                case EFrequency.Annually: return lastProcessed.AddYears(1);
                case EFrequency.BiAnnually: return lastProcessed.AddDays(180);
                case EFrequency.BiMonthly: return lastProcessed.AddMonths(2);
                case EFrequency.BiWeekly: return lastProcessed.AddDays(14);
                case EFrequency.Daily: return lastProcessed.AddDays(1);
                //case EFrequency.Intermittent
                case EFrequency.Monthly: return lastProcessed.AddMonths(1);
                case EFrequency.Quarterly: return lastProcessed.AddMonths(3);
                case EFrequency.Weekly: return lastProcessed.AddDays(7);
                default: return lastProcessed;
            }
        }

        public IDictionary<DateTime, IList<IForecastItem>> GenerateDueDates(IForecast forecast)
        {
            // 1A. Create workspace holders for each forecast item; will be used to determine what days each item will affect the forecast
            List<ForecastItemWorkspace> workspaces = new List<ForecastItemWorkspace>();

            // 1B. We will also cache the forecast items so that we can retrieve them by the key
            Dictionary<Guid, IForecastItem> fcItemCache = new Dictionary<Guid, IForecastItem>();

            foreach (IForecastItem item in forecast.Items)
            {
                workspaces.Add(new ForecastItemWorkspace(item, DateTime.MaxValue, item.Begin));
                fcItemCache.Add(item.Id, item);
            }

            // 2. Prepopulate a collection representing every day within the forecast window (duration of the forecast)
            Dictionary<DateTime, IList<IForecastItem>> values = new Dictionary<DateTime, IList<IForecastItem>>();

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

                if (_verbose) { Console.WriteLine("Analyzing Forecast Item {0}: {1}", i, ws.Name); }

                // reset the counter as we will be iterating every day within the forecast period again
                idx = forecast.Begin;

                while (idx <= forecast.End)
                {
                    if (ws.Due == idx)
                    {
                        if (_verbose)
                        {
                            Console.WriteLine("\t{0}: Due", idx.ToString("dd-MMM"));
                            Console.WriteLine("\t\tvalues.Count: {0}", values[idx].Count);
                        }
                        values[idx].Add(fcItemCache[ws.Id]);
                        if (_verbose) { Console.WriteLine("\t\tvalues.Count: {0}", values[idx].Count); }
                        ws.LastProcessed = idx;
                        ws.Due = this.GetDueDate(ws.LastProcessed, ws.Frequency);
                        if (_verbose) { Console.WriteLine("\t\tNext Due: {0}", ws.Due.ToString("dd-MMM")); }
                    }
                    else
                    {
                        if (_verbose) { Console.WriteLine("\t{0}: Not Due", idx.ToString("dd-MMM")); }
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