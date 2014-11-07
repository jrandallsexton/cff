
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CFF.Enumerations;
using CFF.Interfaces;

using CFF.Tests.Interfaces;

namespace CFF.Tests.Factories
{

    public class ForecastFactory : IForecastFactory 
    {

        public IForecast Create()
        {

            int yr = 2015;
            int duration = 60;

            // Create the forecast
            Forecast forecast = new Forecast("Test CFF", EForecastType.Snapshot, new DateTime(yr, 1, 1), duration);
            forecast.AmountBegin = 1000m;

            // add items to it
            forecast.AddItem(new ForecastItem("Wages", EForecastItemType.Income, EFrequency.Weekly, 1000m, new DateTime(yr, 1, 6)));
            forecast.AddItem(new ForecastItem("Mortgage", EForecastItemType.Expense, EFrequency.Monthly, 850m, new DateTime(yr, 1, 14)));
            forecast.AddItem(new ForecastItem("Cell Phone", EForecastItemType.Expense, EFrequency.Monthly, 135m, new DateTime(yr, 1, 20)));
            forecast.AddItem(new ForecastItem("Car Note", EForecastItemType.Expense, EFrequency.Monthly, 250m, new DateTime(yr, 1, 28)));

            return forecast;
        }

    }

}