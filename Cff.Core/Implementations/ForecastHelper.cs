using System;
using System.Collections.Generic;
using System.Text;
using Cff.Core.Enumerations;
using Cff.Core.Interfaces;
using Cff.Core.Models;

namespace Cff.Core.Implementations
{
    public class ForecastHelper : IForecastHelper
    {

        private readonly bool _verbose = true;
        private readonly bool _logInfo = true;

        public ForecastHelper()
        {

        }

        public ForecastHelper(bool verbose, bool logInfo)
        {
            _verbose = verbose;
            _logInfo = logInfo;
        }

        public DateTime GetDueDate(DateTime lastProcessed, EFrequency frequency)
        {
            switch (frequency)
            {
                case EFrequency.Annually: return lastProcessed.AddYears(1);
                case EFrequency.BiAnnually: return lastProcessed.AddDays(180);
                case EFrequency.BiMonthly: return lastProcessed.AddMonths(2);
                case EFrequency.BiWeekly: return lastProcessed.AddDays(14);
                case EFrequency.Daily: return lastProcessed.AddDays(1);
                case EFrequency.Intermittent: return DateTime.MaxValue;
                case EFrequency.Monthly: return lastProcessed.AddMonths(1);
                case EFrequency.Quarterly: return lastProcessed.AddMonths(3);
                case EFrequency.Weekly: return lastProcessed.AddDays(7);
                default: return lastProcessed;
            }
        }

        public IDictionary<DateTime, IList<ForecastItem>> GenerateDueDates(Forecast forecast)
        {

            // 1A. Create workspace holders for each forecast item; will be used to determine what days each item will affect the forecast
            var workspaces = new List<ForecastItemWorkspace>();

            // 1B. We will also cache the forecast items so that we can retrieve them by the key
            var fcItemCache = new Dictionary<Guid, ForecastItem>();

            foreach (var item in forecast.Items)
            {
                if (item.LastProcessed.HasValue)
                {
                    workspaces.Add(new ForecastItemWorkspace(item, DateTime.MaxValue, this.GetDueDate(item.LastProcessed.Value, item.Frequency)) { Id = item.Id });
                }
                else
                {
                    workspaces.Add(new ForecastItemWorkspace(item, DateTime.MaxValue, item.Begin) { Id = item.Id });
                }

                //workspaces.Add(new ForecastItemWorkspace(item, item.Begin, item.Begin));
                fcItemCache.Add(item.UId, item);
            }

            // 2. Pre-populate a collection representing every day within the forecast window (duration of the forecast)
            var values = new Dictionary<DateTime, IList<ForecastItem>>();

            for (var idxx = forecast.Begin; idxx <= forecast.End; idxx = idxx.AddDays(1d))
            {
                values.Add(idxx, new List<ForecastItem>());
            }

            // 3. Iterate through each workspace, determine each day it needs to be processed and add it to the return collection
            for (var i = 0; i < workspaces.Count; i++)
            {

                var ws = workspaces[i];

                if (_verbose) { Console.WriteLine("Analyzing Forecast Item {0}: {1} -> {2}", i, ws.Name, ws.Amount); }

                // reset the counter as we will be iterating every day within the forecast period again
                var idx = forecast.Begin;

                while (idx <= forecast.End)
                {

                    if (ws.Due == idx)
                    {

                        if (_verbose)
                        {
                            Console.WriteLine("\t{0}: Due", idx.ToString("dd-MMM"));
                            Console.WriteLine("\t\tvalues.Count: {0}", values[idx].Count);
                        }

                        values[idx].Add(fcItemCache[ws.UId]);

                        if (_verbose) { Console.WriteLine("\t\tvalues.Count: {0}", values[idx].Count); }

                        ws.LastProcessed = idx;

                        ws.Due = this.GetDueDate(ws.LastProcessed, ws.Frequency);
                        if (ws.Due > ws.End) { break; }

                        if (_verbose) { Console.WriteLine("\t\tNext Due: {0}", ws.Due.ToString("dd-MMM")); }

                    }
                    else
                    {
                        if (_verbose && _logInfo) { Console.WriteLine("\t{0}: Not Due", idx.ToString("dd-MMM")); }
                    }

                    idx = idx.AddDays(1d);

                }

            }

            return values;

        }

    }
}
