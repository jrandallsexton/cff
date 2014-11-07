
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;
using CFF.Interfaces;

namespace CFF.Engines
{

    public class RoughEngine : IForecastEngine 
    {

        private bool _verbose = false;

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
            IDictionary<DateTime, IList<IForecastItem>> values = this._helper.GenerateDueDates(forecast);

            DateTime idx = forecast.Begin;
            decimal amtBegin = forecast.AmountBegin;
            decimal amtEnd = forecast.AmountBegin;

            while (idx <= forecast.End)
            {

                // process items that need processing
                if ((values.ContainsKey(idx)) && (values[idx].Count > 0))
                {

                    if (_verbose) { Console.WriteLine("{0}: {1} transactions found.", idx.ToString("dd-MMM-yyyy"), values[idx].Count); }

                    // we found items that needed to be processed on the current day
                    foreach (var item in values[idx])
                    {
                        if (item.Type == EForecastItemType.Income)
                        {
                            if (_verbose) { Console.WriteLine("\tIncome: {0:C}\t{1}", item.Amount, item.Name); }
                            amtBegin += item.Amount;
                        }
                        else
                        {
                            if (_verbose) { Console.WriteLine("\tExpense: {0:C}\t{1}", item.Amount, item.Name); }
                            amtBegin -= item.Amount;
                        }
                    }

                }
                else
                {
                    if (_verbose) { Console.WriteLine("{0}: 0 transactions found.", idx.ToString("dd-MMM-yyyy")); }
                }

                // store the daily result
                result.Results.Add(idx, new ForecastResultItem(amtBegin, amtBegin));

                //amtBegin = amtEnd;

                idx = idx.AddDays(1d);
            }

            result.AmountEnd = amtBegin;

            return result;
        }

    }



}